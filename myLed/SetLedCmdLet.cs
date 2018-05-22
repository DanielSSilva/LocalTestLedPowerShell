using System;
using System.Drawing;
using System.Management.Automation;  // PowerShell namespace.
namespace WS281x.CmdLets
{
	
	[Cmdlet(VerbsCommon.Set, "SingleLedColor")]
	public class SetSingleLedColor : Cmdlet , IDisposable
	{
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public Color Color {get; set;}

		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 1)]
		public int LedId {get; set;}

		[Parameter(Mandatory = false)]
		public byte Brightness {get; set;}

		[Parameter(Mandatory = false)]
		public SwitchParameter Invert {get;set;}

		[Parameter(Mandatory = false)]
		public int GpioPin {get; set;}
		private WS281x controller;

		public SetSingleLedColor()
		{
			this.Brightness = 255;
			this.Invert = false;
			this.GpioPin = 18;
			Settings settings = Settings.CreateDefaultSettings();
            settings.Channels[0] = new Channel(1, this.GpioPin, this.Brightness, this.Invert, StripType.WS2812_STRIP);
			controller = new WS281x(settings);
		}

		protected override void	ProcessRecord()
		{
			controller.SetLEDColor(this.LedId, this.Color);
			controller.Render();
		}

		public void Dispose()
		{
			controller.Dispose();
		}
	}
}