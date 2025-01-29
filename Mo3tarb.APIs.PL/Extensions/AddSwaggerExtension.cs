namespace Mo3tarb.Extensions
{
	public static class AddSwaggerExtension
	{
		public static WebApplication UseSwaggerMiddelware(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;

		}
	}
}
