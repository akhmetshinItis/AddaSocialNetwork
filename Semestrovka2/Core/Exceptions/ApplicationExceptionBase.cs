namespace Core.Exceptions
{
	/// <summary>
	/// Базовое исключение для логики приложения
	/// </summary>
	public class ApplicationExceptionBase : ApplicationException
	{
		/// <summary>
		/// Базовое исключение для логики приложения
		/// </summary>
		public ApplicationExceptionBase()
		{
		}

		/// <summary>
		/// Базовое исключение для логики приложения
		/// </summary>
		/// <param name="message">Сообщение</param>
		public ApplicationExceptionBase(string message)
			: base(message)
		{
		}
	}
}
