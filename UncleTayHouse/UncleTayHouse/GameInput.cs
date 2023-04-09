namespace UncleTayHouse
{
	internal class GameInput
	{
	}
	public partial class Game
	{
		public string ReadInput(string prompt)
		{
			Console.Write(prompt);
			var res = Console.ReadLine();
			if (res == null || res.Length == 0 || res.Length > 100)
				return String.Empty;

			res = res.ToUpper();

			if (res == "EXIT")
			{
				Environment.Exit(0);
			}
			return res;
		}
		public void ProcessInput(string prompt)
		{
			// reset previous input
			for (int i = 0; i < MaxInput; i++)
			{
				InputWords_INWS[i] = "";
				InputWordNum_INPTK[i] = 0;
			}

			prompt = prompt.ToUpper();
			int idx = 0;

			// split input:
			var words = prompt.Split(" ");

			InputWords = words.Length; // used later

			for (var i = 0; i < words.Length; i++)
			{
				// remove null words
				for (var j = 0; j < NULLWORDS.Length; j++)
				{
					if (words[i] == NULLWORDS[j])
					{
						words[i] = "";
					}
				}
				// only add if not null
				if (words[i] != "")
				{
					// find the verb number for this word
					for (var k = 0; k < VOCABS.Length; k++)
					{
						// only add if it is a known word
						if (words[i] == VOCABS[k])
						{
							idx++;
							InputWords_INWS[idx] = VOCABS[k]; // words[i] or VOCABS[k]
							InputWordNum_INPTK[idx] = k;
							break;
						}
						//if (InputWords_INWS[idx] == VOCABS[k])
						//{
						//	InputWordNum_INPTK[idx] = k;
						//	break;
						//}
					}
					//idx++;
				}
				// InputWords_INWS contain only valid words now
				// InputWordNum_INPTK contain the verb number
			}
		}
	}
}
