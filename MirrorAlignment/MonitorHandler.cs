using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;
using SwapChain = SlimDX.DXGI.SwapChain;
using Viewport = SlimDX.Direct3D11.Viewport;

namespace MirrorAlignmentSystem
{
	/// <summary>
	///  This class handles the TV and it's image by using DirectX 
	/// </summary>
	public class MonitorHandler
	{
		RenderForm Form;
		DeviceContext context;
		Viewport viewport;
		SwapChain swapChain;
		Device device;
		RenderTargetView renderTarget;
		VertexShader vertexShader;
		PixelShader pixelShader;
		ShaderSignature inputSignature;
		ShaderResourceView resourceView;
		DataStream vertices;
		SlimDX.Direct3D11.Buffer vertexBuffer;

		bool calibrateOrNot = false;
		//volatile bool vectorWriteReadOnGoing = false;
		static Mutex vectorLock = new Mutex();

		/// <summary>
		/// The class constructor
		/// </summary>
		public MonitorHandler() 
		{
		}

		/// <summary>
		/// Let the class know what kind of image to render on the TV
		/// </summary>
		/// <param name="input">Sets the variable which tells the TV if the image is supose to be black or a bitmap</param>    
		public void SetCalibrateOrNot(bool input) 
		{
			calibrateOrNot = input;
		}

		/// <summary>
		/// Let the class know what vectors to render, this method is not yet implemented
		/// </summary>
		public void SetVectors()
		{
		}

		/// <summary>
		/// The MonitorHandler has it's own thread. This is the entry point for this thread.
		/// </summary>
		public void Run() 
		{
			Form = new RenderForm("MAS");
			var description = new SwapChainDescription()
			{
				BufferCount = 1,
				Usage = SlimDX.DXGI.Usage.RenderTargetOutput,
				OutputHandle = Form.Handle,
				IsWindowed = true,
				ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), SlimDX.DXGI.Format.R8G8B8A8_UNorm),
				SampleDescription = new SampleDescription(1, 0),
				Flags = SwapChainFlags.AllowModeSwitch,
				SwapEffect = SlimDX.DXGI.SwapEffect.Discard
			};
            
			Form.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Form.FormBorderStyle = FormBorderStyle.None;

			Screen[] screens = Screen.AllScreens;

			int sec_idx = 0;
			for (int i = 0; i < screens.Length; ++i)
			{
				if (!screens[i].Primary)
				{
					sec_idx = i;
					break;
				}
			}
			Point location = screens[sec_idx].Bounds.Location;

			Form.Left = location.X;
			Form.Top = location.Y;
			Form.Width  = screens[sec_idx].Bounds.Width;
			Form.Height = screens[sec_idx].Bounds.Height;

			Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, description, out device, out swapChain);

			using (var resource = Resource.FromSwapChain<Texture2D>(swapChain, 0))
				renderTarget = new RenderTargetView(device, resource);

			context = device.ImmediateContext;
			viewport = new Viewport(0.0f, 0.0f, Form.Width, Form.Height);
			context.OutputMerger.SetTargets(renderTarget);
			context.Rasterizer.SetViewports(viewport);
			//ShaderBytecode effectByteCode = ShaderBytecode.CompileFromFile("triangle.fx", "Render", "fx_5_0", ShaderFlags.None, EffectFlags.None);
			//Effect effect = new Effect(device, effectByteCode);

			SamplerDescription sampleDesc = new SamplerDescription();
			sampleDesc.AddressU = TextureAddressMode.Wrap;
			sampleDesc.AddressV = TextureAddressMode.Wrap;
			sampleDesc.AddressW = TextureAddressMode.Wrap;

			sampleDesc.Filter = Filter.MinPointMagMipLinear;

			SamplerState samplerState = SamplerState.FromDescription(device, sampleDesc);

			Texture2D calibrateTexture = Texture2D.FromFile(device, "c:\\Visningsbilder\\cross.png");
			Texture2D whiteTexture = Texture2D.FromFile(device, "c:\\Visningsbilder\\White_Background.png");

			//effect.GetVariableByName("xTexture").AsResource().SetResource(resourceView);
			//effect.GetVariableByName("TextureSampler").AsSampler().SetSamplerState(0, samplerState);

			context.PixelShader.SetSampler(samplerState, 0);

			//Sets up the vertex shader
			using (var bytecode = ShaderBytecode.CompileFromFile("triangle.fx", "vs_main", "vs_4_0", ShaderFlags.None, EffectFlags.None))
			{
				inputSignature = ShaderSignature.GetInputSignature(bytecode);
				vertexShader = new VertexShader(device, bytecode);
			}

			//Sets up the pixel shader
			using (var bytecode = ShaderBytecode.CompileFromFile("triangle.fx", "ps_main", "ps_5_0", ShaderFlags.None, EffectFlags.None))
				pixelShader = new PixelShader(device, bytecode);

			//Initializing the vertices
			vertices = new DataStream(20 * 4, true, true);
			vertices.Write(new Vector3(-0.25f, -0.25f, 0.0f)); vertices.Write(new Vector2(1.0f, 1.0f));
			vertices.Write(new Vector3(-0.25f, 0.25f, 0.0f)); vertices.Write(new Vector2(0.0f, 1.0f));
			vertices.Write(new Vector3(0.25f, -0.25f, 0.0f)); vertices.Write(new Vector2(1.0f, 0.0f));
			vertices.Write(new Vector3(0.25f, 0.25f, 0.0f)); vertices.Write(new Vector2(0.0f, 0.0f));
			vertices.Position = 0;

			var elements = new[] { new InputElement("POSITION", 0, Format.R32G32B32_Float, 0), new InputElement("textcoord", 0, Format.R32G32_Float, 12, 0) };
			var layout = new InputLayout(device, inputSignature, elements);
			vertexBuffer = new SlimDX.Direct3D11.Buffer(device, vertices, 20 * 4, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

			context.InputAssembler.InputLayout = layout;
			context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
			context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, 20, 0));

			context.VertexShader.Set(vertexShader);
			context.PixelShader.Set(pixelShader);

			Form.Show();

			Stopwatch stopWatchGraphics = new Stopwatch();

			//This is the loop, everytime this is run a new image is rendered to the TV
			MessagePump.Run(Form, () =>
			{
				//stopWatchGraphics.Reset();
				//stopWatchGraphics.Start();

				vectorLock.WaitOne();

				context.ClearRenderTargetView(renderTarget, new Color4(0.0f, 0.0f, 0.0f));

				//This if statement checks if the user have choosen to calibrate or not and in that case which texture to use
				if (calibrateOrNot)
				{
                    resourceView = new ShaderResourceView(device, whiteTexture);
				}
				else
				{
					resourceView = new ShaderResourceView(device, whiteTexture);
				}

				device.ImmediateContext.PixelShader.SetShaderResource(resourceView, 0);

				context.Draw(4, 0);

				//Switch between the background image and the current image presented to the user
				swapChain.Present(0, SlimDX.DXGI.PresentFlags.None);

				resourceView.Dispose();

				vectorLock.ReleaseMutex();

				//stopWatchGraphics.Stop();
				//System.Diagnostics.Debug.WriteLine("Graphics time elapsed: " + stopWatchGraphics.ElapsedMilliseconds + "ms");
			});

			//Desposes all the resources that has been used during the execution of the application
			vertices.Close();
			vertexBuffer.Dispose();
			layout.Dispose();
			inputSignature.Dispose();
			vertexShader.Dispose();
			pixelShader.Dispose();
			renderTarget.Dispose();
			swapChain.Dispose();
			device.Dispose();
		}

		/// <summary> Moves the vertices off the screen so that a blackscreen is rendered. </summary>
		public void BlackScreen() 
		{
			vectorLock.WaitOne();

			vertices = null;

			vertices = new DataStream(20 * 4, true, true);

			vertices.Write(new Vector3(-2, -2, 0.0f)); vertices.Write(new Vector2(1.0f, 1.0f));
			vertices.Write(new Vector3(-2, -2, 0.0f)); vertices.Write(new Vector2(0.0f, 1.0f));
			vertices.Write(new Vector3(-2, -2, 0.0f)); vertices.Write(new Vector2(1.0f, 0.0f));
			vertices.Write(new Vector3(-2, -2, 0.0f)); vertices.Write(new Vector2(0.0f, 0.0f));

			//Sets the pointer to 0 so that the application is reading from the start
			vertices.Position = 0;

			//Updates the vertex buffer
			vertexBuffer = new SlimDX.Direct3D11.Buffer(device, vertices, 20 * 4, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
			context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, 20, 0));

			vectorLock.ReleaseMutex();
		}

		/// <summary>
		/// Updates the the vertices to show a pattern depending on which segment number is currently being worked on
		/// </summary>
		/// <param name="doublePoints">The points for the pattern</param>    
		/// <param name="fine">True if fine alignment is used, false if coarse alignment is used</param> 
		public void UpdatePatternVertices(double[,] doublePoints, bool fine) 
		{
			vectorLock.WaitOne();

			vertices = null;

			int verticesSize;

			//The application only uses 3 points on the monitor in coarse mode while it uses 4 in fine mode
			if (fine)
			{
				verticesSize = 16;
			}
			else
			{
				verticesSize = 12;
			}

			//Updates the vertices datastream to the new size decided above
			vertices = new DataStream(20 * verticesSize, true, true);

			if (fine)
			{
				//MessageBox.Show(points[0].X + " " + points[0].Y);

				//MessageBox.Show("Position: " + vertices.Position + " Vertices size: " + verticesSize + " " + doublePoints[0, 0] + " " + doublePoints[0, 1] + " " + doublePoints[1, 0] + " " + doublePoints[1, 1] + " " + doublePoints[2, 0] + " " + doublePoints[2, 1] + " " + doublePoints[3, 0] + " " + doublePoints[3, 1]);
				vertices.Write(new Vector3((float)doublePoints[1, 0], (float)doublePoints[1, 1], 0.0f)); vertices.Write(new Vector2(1.0f, 1.0f));
				vertices.Write(new Vector3((float)doublePoints[0, 0], (float)doublePoints[0, 1], 0.0f)); vertices.Write(new Vector2(0.0f, 1.0f));
				vertices.Write(new Vector3((float)doublePoints[3, 0], (float)doublePoints[3, 1], 0.0f)); vertices.Write(new Vector2(1.0f, 0.0f));
				vertices.Write(new Vector3((float)doublePoints[2, 0], (float)doublePoints[2, 1], 0.0f)); vertices.Write(new Vector2(0.0f, 0.0f));

				//vertices.Position = 0;
			}
			else
			{
				//        System.Diagnostics.Debug.WriteLine("");
				//        System.Diagnostics.Debug.WriteLine("(" + x1 + "." + y1 + ") (" + x2 + "." + y2 + ") (" + x3 + "." + y3 + ") (" + x4 + "." + y4 + ")");
				//        System.Diagnostics.Debug.WriteLine("");
				//        //MessageBox.Show(x1 + "," + y1 + " " + x2 + "," + y2 + " " + x3 + "," + y3 + " " + x4 + "," + y4);

				//        vertices.Write(new Vector3(x1, y1, 0.0f)); vertices.Write(new Vector2(1.0f, 1.0f));
				//        vertices.Write(new Vector3(x2, y2, 0.0f)); vertices.Write(new Vector2(0.0f, 1.0f));
				//        vertices.Write(new Vector3(x3, y3, 0.0f)); vertices.Write(new Vector2(1.0f, 0.0f));
				//        vertices.Write(new Vector3(x4, y4, 0.0f)); vertices.Write(new Vector2(0.0f, 0.0f));
				vertices.Write(new Vector3((float)doublePoints[1, 0], (float)doublePoints[1, 1], 0.0f)); vertices.Write(new Vector2(1.0f, 1.0f));
				vertices.Write(new Vector3((float)doublePoints[0, 0], (float)doublePoints[0, 1], 0.0f)); vertices.Write(new Vector2(0.0f, 1.0f));
				vertices.Write(new Vector3((float)doublePoints[2, 0], (float)doublePoints[2, 1], 0.0f)); vertices.Write(new Vector2(1.0f, 0.0f));
			}

			vertices.Position = 0;

			vertexBuffer = new SlimDX.Direct3D11.Buffer(device, vertices, 20 * verticesSize, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
			context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, 20, 0));

			vectorLock.ReleaseMutex();

		}
	}
}
