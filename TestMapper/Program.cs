using System;

namespace TestMapper
{

	class Program
	{
		public static void Main()
		{
			Console.WriteLine("Merry Christmas");
			Console.ReadLine();
		}

		static int RaiseToPower(int n, int power)
		{
			if (power == 0) return 1;

			return RaiseToPower(n, power - 1) * n;
		}
	}
}
