
public interface IA1
{
}

public class A1 : IA1
{
    public void PlayMusic()
    {
        Console.WriteLine("hahahah");
    }
}


public class B
{
    A1 a = new A1();

    public void PlayMusic()
    {
        a.PlayMusic();
    }
}