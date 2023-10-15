namespace Lab9;

public class Client
{
    private double callTime; //
    private double penalty;
    private double debt;
    private double price;

    private InitCl clientInfo;
    
    private TypeOfConnection toc;
    
    private int debtDays;
    
    
    public Client()
    {
        
    }

    public void showInfo()
    {
        Console.WriteLine($"Client: {clientInfo.initials}\nNumber: {clientInfo.number}\nCall Time: {this.callTime}\nDept: {this.debt}\nPenalty: {this.penalty}\nPrice: {this.price}");
    }
    
    struct InitCl
    {
        public string initials;
        public string number;

        public InitCl(string initials, string number)
        {
            this.initials = initials;
            this.number = number;
        }
    }
}

enum TypeOfConnection
{
    Wired,
    Wireless
}