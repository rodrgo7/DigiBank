namespace DigiBank.Classes
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente()
        {
        this.NumConta = "00" + Conta.NumContaSequencial;
        }
    }
}