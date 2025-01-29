class Solution
{

  static void rotateMatrice(int[,] mat, int m, int n, int r)
  {

    int mini = Math.Min(m, n);
    int iterations = mini / 2;

    for (int i = 0; i < m; i++)
    {
      string s = "";
      for (int j = 0; j < n; j++)
      {
        s += mat[i, j].ToString();
        s += " ";
      }
      Console.WriteLine(s);
    }

    Console.WriteLine("\n\n");

    for (int k = 0; k < r; k++)
    {
      for (int i = 0; i < iterations; i++)
      {

        int startI = i, startJ = i;
        int borderI = m - i - 1, borderJ = n - i - 1;
        int direction = 1;
        int nextElement = 0;
        int next = 0, previous = mat[i, i];
        int I = startI, J = startJ;
        while (true)
        {
          if (direction == 1)
          {
            I += 1;
          }
          else if (direction == 2)
          {
            J += 1;
          }
          else if (direction == 3)
          {
            I -= 1;
          }
          else
          {
            J -= 1;
          }

          next = mat[I, J];
          mat[I, J] = previous;
          previous = next;

          if (direction == 1 && I == borderI)
          {
            direction = 2;
          }
          else if (direction == 2 && J == borderJ)
          {
            direction = 3;
          }
          else if (direction == 3 && I == startJ)
          {
            direction = 4;
          }
          else if (I == startI && J == startJ)
          {
            break;
          }
        }
      }
    }

    for (int i = 0; i < m; i++)
    {
      string s = "";
      for (int j = 0; j < n; j++)
      {
        s += mat[i, j].ToString();
        s += " ";
      }
      Console.WriteLine(s);
    }
  }
  
   public static void Main(string[] args)
   {

    List<int> data = new List<int>(){ 5, 1, 3, 6, -1, 9 };
    var result = MaxProfit(data);

    int[,] mat =
    {
      {1,2,3,4,7 } ,{8,9,10,13,14 } , {15,16, 19,20, 21 }, {22, 25,26,27,28}
    };

    rotateMatrice(mat, 4, 5, 2);
   
  }
}