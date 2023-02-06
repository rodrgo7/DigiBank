using DigiBank.Contratos;

namespace DigiBank.Classes
{
    public abstract class Conta : Banco, IConta
    {
        public Conta()
        {
            this.NumAgencia = "0001";
            Conta.NumContaSequencial++;
            this.Movimentacoes = new List<Extrato>();
        }

        public double Saldo {get; protected set;}
        public string NumAgencia {get; private set;}
        public string NumConta {get; protected set;}
        public static int NumContaSequencial { get; private set;}
        private List<Extrato> Movimentacoes;
        public double ConsultaSaldo()
        {
            return this.Saldo;
        }
        public void Deposita(double valor)
        {
            DateTime dataAtual = DateTime.Now;
            this.Movimentacoes.Add(new Extrato(dataAtual, "Deposito", valor));

            this.Saldo += valor;
        }
        public bool Saca(double valor)
        {
            if(valor > this.ConsultaSaldo())
            return false;            

            DateTime dataAtual = DateTime.Now;
            this.Movimentacoes.Add(new Extrato(dataAtual, "Saque", valor));

            this.Saldo -= valor;
            return true;
        }
        public string GetCodigoBanco()
        {
            return this.CodigoBanco;
        }
        public string GetNumeroAgencia()
        {
            return this.NumAgencia;
        }
        public string GetNumeroConta()
        {
            return this.NumConta;
        }
        public List<Extrato> Extrato()
        {
            return this.Movimentacoes;
        }
    }
}