

public class Block
{
  public List<String> Data
  {
    get; set;
  }

  public byte[]? HASH { get; set; }
  public byte[]? LAST_HASH { get; set; }
  public int Difficulty { get; set; }

  public int Nonce { get; set; }
  public long Timestamp { get; set; }
}

static public class Miner
{
  static public byte[] hashBlock(Block block)
  {
    byte[] hash;
    using (MemoryStream mem = new MemoryStream())
    {
      mem.Write(block.HASH);
      mem.Write(Encoding.UTF8.GetBytes(block.Nonce.ToString()));
      mem.Write(Encoding.UTF8.GetBytes(String.Join("", block.Data)));
      mem.Write(Encoding.UTF8.GetBytes(block.Timestamp.ToString()));
      hash = mem.ToArray();
    }
    
    hash = MD5.HashData(hash);
    return hash;
  }

  static public Block blockGenesis()
  {
    return new Block()
    {
      Timestamp = 1,
      Difficulty = 3,
      LAST_HASH = null,
      HASH = null,
      Data = null
    };
  }

  static public Block mineBlock(Block lastBlock, List<String> newData)
  {
    var newBlock = new Block()
    {
      Data = newData,
      LAST_HASH = lastBlock.HASH,
      Timestamp = DateTime.Now.Ticks,
      Nonce = 0
    };

    byte[] computedHash;
    int n = lastBlock.Difficulty;
    int count;
    while (true)
    {
      computedHash = hashBlock(newBlock);

      string hashedData = Encoding.UTF8.GetString(computedHash);
      if (hashedData.Length > n)
      {
        count = 0;
        foreach (char c in hashedData.Take(n))
        {
          if (c == '0')
          {
            count++;
          }
          else
          {
            break;
          }
        }

        if (count == n)
        {
          break;
        }
        else
        {
          newBlock.Timestamp = DateTime.Now.Ticks;
          newBlock.Nonce++;
        }
      }
    }

    TimeSpan duration = TimeSpan.FromTicks(lastBlock.Timestamp) - TimeSpan.FromTicks(newBlock.Timestamp);

    if (duration.TotalMinutes > 10)
    {
      newBlock.Difficulty--;
    }
    else
    {
      newBlock.Difficulty++;
    }

    newBlock.HASH = computedHash;
    return newBlock;
  }

}

public class BlockChain
{
  public List<Block> blocks { get; set; }

  public BlockChain()
  {
    blocks = new List<Block>() { Miner.blockGenesis() };
  }

  public void addBlock(Block block)
  {
    blocks.Add(block);
  }

  public void printBlockChain()
  {
    Console.WriteLine("List blocks: \n");

    foreach (var block in blocks)
    {
      Console.WriteLine(block.Data == null ? "empty" : String.Join(" ", block.Data));
      Console.WriteLine(block.HASH == null ? "empty" : Encoding.UTF8.GetString(block.HASH));
      Console.WriteLine(block.LAST_HASH == null ? "empty" : Encoding.UTF8.GetString(block.LAST_HASH));
      Console.WriteLine(block.Timestamp.ToString());
      Console.WriteLine(block.Difficulty);
      Console.WriteLine(block.Nonce);
      Console.WriteLine("\n");
      Console.WriteLine("\n");
    }

  }
}


class Solution
{
  public static void Main(string[] args)
  {

    BlockChain bc = new BlockChain();

    List<string> transactions = new List<string>() { "transaction 1", "transaction 2", "transaction 3", "transaction 4", "transaction 5", "transaction 6", };
    Block block1 = Miner.mineBlock(bc.blocks.Last(), transactions.GetRange(0, 2));
    bc.addBlock(block1);
    Block block2 = Miner.mineBlock(bc.blocks.Last(), transactions.GetRange(2, 2));
    bc.addBlock(block2);
    Block block3 = Miner.mineBlock(bc.blocks.Last(), transactions.GetRange(4, 2));
    bc.addBlock(block3);

    bc.printBlockChain();

  }
}