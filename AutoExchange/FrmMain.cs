using BinanceExchange.API.Client;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Extensions;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;
using BinanceExchange.API.Models.Response.Error;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AutoExchange
{
    public partial class  AutoExchange : Form
    {

        int request;
        Timer timer1;
        public AutoExchange()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            request = 0;
        }


        public async void Teste()
        {
           
            StringBuilder txt;
            string apiKey;
            string secretKey;

            decimal startCurrence;
            decimal fee;

            string m1;
            string m2;
            decimal soma1;
            decimal soma2;
            decimal final;

            decimal dsoma1;
            decimal dsoma2;
            decimal dfinal;

            string p1;
            string p2;
            long timestamp1;
            long timestamp2;

            bool Valid;
            bool Auto;

            int decPart;
            IEnumerable<ExchangeInfoSymbol> tSymbol;
            ExchangeInfoSymbolFilterLotSize lotSize1;
            ExchangeInfoSymbolFilterLotSize lotSize2;
            ExchangeInfoSymbolFilterLotSize lotSize3;
            ExchangeInfoSymbolFilterPrice price3;

            try
            {

                Cursor.Current = Cursors.WaitCursor;
                btnRun.Enabled = false;
                btnRun.Text = "Runing!!";
                txbResult.Text = "";

                apiKey = txbAPIKey.Text.Trim();
                secretKey = txbSecretKey.Text.Trim();
                Auto = chbAutoNeg.Checked;
                txt = new StringBuilder();

                txt.AppendLine("--------------------------");
                txt.AppendLine("BinanceExchange API - Tester");
                txt.AppendLine("--------------------------");

                txbResult.Text += txt;

                //Building a test logger
                var ProgramLogger = LogManager.GetLogger(typeof(AutoExchange));
                ProgramLogger.Debug("Logging Test");

                timer1 = new Timer();
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Interval = 60000; // in miliseconds
                timer1.Start();

                //Initialise the general client client with config
                var client = new BinanceClient(new ClientConfiguration()
                {
                    ApiKey = apiKey,
                    SecretKey = secretKey,
                    Logger = ProgramLogger,
                });

                request += 1;

                txt = new StringBuilder();
                txt.AppendLine("Get BTC Funds: " + chbUseCurrBTC.Checked);
                txt.AppendLine("Automatic Negociations: " + Auto);
                txt.AppendLine("Interacting with Binance...");
                txbResult.Text += txt;

                // Set TimestampOffset for ajust hour with server
                txbResult.AppendText("Checking the time with the server...\r\n");

#if NETSTANDARD2_0
                timestamp1 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
#else
                timestamp1 = DateTime.UtcNow.ConvertToUnixTime();
#endif

                var serverTime = await client.GetServerTime();

                request += 1;

                timestamp2 = serverTime.ServerTime.ConvertToUnixTime();

                client.TimestampOffset = System.TimeSpan.FromMilliseconds(timestamp2-timestamp1);

                if (chbUseCurrBTC.Checked)
                {
                    txbResult.AppendText("Get funds from BTC Funds account (Online)...\r\n");

                    // Get account information
                    var accountInformation = await client.GetAccountInformation(3500);

                    request += 1;

                    var balances = accountInformation.Balances;

                    foreach (var balance in balances)
                    {
                        if (balance.Asset == "BTC")
                        {
                            txbInBTC.Text = balance.Free.ToString();
                            txbResult.AppendText("Found BTC amout: " + balance.Free + "\r\n");
                        }
                    } 

                }

                txbResult.AppendText("Get Exchange Info...\r\n");

                var excInfo = await client.GetExchangeInfo();
                request += 1;
                List<SymbolPriceChangeTickerResponse24> bnb;

                do
                {
                    txbResult.AppendText("Start Negociations Automatoc Run: "+chbAutoRun.Checked+ "...\r\n");
                    bnb = await client.GetDailyTicker24();
                    request += 1;

                    if (bnb.Count <= 0)
                        return;

                    startCurrence = 0;            
                    fee = Convert.ToDecimal(txbFeeTax.Text.Trim())/100;

                    //Só termina com BTC
                    foreach (var par in bnb)
                    {
                        Valid = true;
                        startCurrence = Convert.ToDecimal(txbInBTC.Text.Trim());
                    
                        m1 = "";
                        m2 = "";
                        if (par.symbol.Length == 5)
                        {
                            m1 = par.symbol.Substring(0, 2);
                            m2 = par.symbol.Substring(2, 3);
                        }
                        else if (par.symbol.Length == 6)
                        {
                            m1 = par.symbol.Substring(0, 3);
                            m2 = par.symbol.Substring(3, 3);
                        }
                        else if (par.symbol.Length == 7)
                        {
                            m1 = par.symbol.Substring(0, 4);
                            m2 = par.symbol.Substring(4, 3);
                        }
                        else if (par.symbol.Length == 8)
                        {
                            m1 = par.symbol.Substring(0, 5);
                            m2 = par.symbol.Substring(5, 3);
                        }
                        else if (par.symbol.Length == 9)
                        {
                            if (par.symbol.StartsWith("BCHABC"))
                            {
                                m1 = par.symbol.Substring(0, 6);
                                m2 = par.symbol.Substring(6, 3);
                            }
                            else
                            {
                                m1 = par.symbol.Substring(0, 5);
                                m2 = par.symbol.Substring(5, 4);
                            }
                        }
                        else if (par.symbol.Length == 10)
                        {
                            m1 = par.symbol.Substring(0, 6);
                            m2 = par.symbol.Substring(6, 4);
                        }

                        tSymbol = (excInfo.Symbols.Where(x => x.Symbol == par.symbol));

                        lotSize1 = findLotSize(tSymbol);

                        if (lotSize1 == null)
                        {
                            throw new Exception("Error to search ExchangeInfoSymbolFilterLotSize");
                        }

                        if (par.AskPrice == 0)
                            continue;

                        if (Convert.ToDouble(lotSize1.MinQty).ToString("0." + new string('#', 20)).Contains("."))
                            decPart = Convert.ToDouble(lotSize1.MinQty).ToString("0." + new string('#', 20)).Substring(lotSize1.MinQty.ToString().IndexOf(".") + 1).Length;
                        else
                            decPart = 0;

                        soma1 = Math.Round(startCurrence / par.AskPrice, decPart);

                        if (soma1 < lotSize1.MinQty && soma1 > lotSize1.MaxQty)
                        {
                            txbResult.AppendText("Invalide quantity for " + par.symbol + " MinQty: " + lotSize1.MinQty + " MaxQty: " + lotSize1.MaxQty + "\r\n");
                            continue;
                        }        

                        if (par.askQty <= soma1)
                            Valid = false;                 

                        var pares = bnb.Where(x => x.symbol.StartsWith(m1) && !x.symbol.Contains(par.symbol) && !x.symbol.Contains("USD")).ToList();

                        dsoma1 = soma1 - Math.Round(soma1 * fee, 8);

                        if (Convert.ToDouble(dsoma1).ToString().Contains("."))
                            dsoma1 = Convert.ToDecimal(dsoma1.ToString().Substring(0,dsoma1.ToString().IndexOf(".")+1 + decPart));

                        foreach (var parMoeda1 in pares)
                        {
                            txt = new StringBuilder();
                            txt.AppendLine("==============================================================================");
                            txt.AppendLine("Exchange: " + par.symbol + " - Buy Price: " + par.AskPrice + m2 + " - Total Coin: " + dsoma1 + m1);

                            soma2 = 0;

                            if (par.BidPrice == 0)
                                continue;

                            tSymbol = (excInfo.Symbols.Where(x => x.Symbol == parMoeda1.symbol));

                            lotSize2 = findLotSize(tSymbol);

                            if (Convert.ToDouble(lotSize2.MinQty).ToString("0." + new string('#', 20)).Contains("."))
                                decPart = Convert.ToDouble(lotSize2.MinQty).ToString("0." + new string('#', 20)).Substring(lotSize2.MinQty.ToString().IndexOf(".") + 1).Length;
                            else
                                decPart = 0;

                            if (parMoeda1.BidPrice > 1)
                            {
                                soma2 = Math.Round(dsoma1 / parMoeda1.BidPrice, decPart);
                            }
                            else
                            {
                                soma2 = Math.Round(dsoma1 * parMoeda1.BidPrice, decPart);
                            }

                            if (soma2 < lotSize2.MinQty && soma2 > lotSize2.MaxQty)
                            {
                                txbResult.AppendText("Invalide quantity for " + parMoeda1.symbol + " MinQty: " + lotSize2.MinQty + " MaxQty: " + lotSize2.MaxQty + "\r\n");
                                continue;
                            }                        
                                            

                            if (parMoeda1.askQty <= soma2)
                                Valid = false;                           

                            dsoma2 = soma2 - Math.Round(soma2 * fee, 8);

                            if (Convert.ToDouble(dsoma2).ToString().Contains("."))
                                dsoma2 = Convert.ToDecimal(dsoma2.ToString().Substring(0, dsoma2.ToString().IndexOf(".") + 1 + decPart));

                            txt.AppendLine("Exchange: " + parMoeda1.symbol + " - Buy Price: " + parMoeda1.BidPrice + m1 + " - Total Coin: " + dsoma2 + parMoeda1.symbol.Replace(m1,""));

                            p1 = "";
                            p2 = "";
                            if (parMoeda1.symbol.Length == 5)
                            {
                                p1 = parMoeda1.symbol.Substring(0, 2);
                                p2 = parMoeda1.symbol.Substring(2, 3);
                            }
                            else if (parMoeda1.symbol.Length == 6)
                            {
                                p1 = parMoeda1.symbol.Substring(0, 3);
                                p2 = parMoeda1.symbol.Substring(3, 3);
                            }
                            else if (parMoeda1.symbol.Length == 7)
                            {
                                p1 = parMoeda1.symbol.Substring(0, 4);
                                p2 = parMoeda1.symbol.Substring(4, 3);
                            }
                            else if (parMoeda1.symbol.Length == 8)
                            {
                                p1 = parMoeda1.symbol.Substring(0, 5);
                                p2 = parMoeda1.symbol.Substring(5, 3);
                            }
                            else if (parMoeda1.symbol.Length == 9)
                            {
                                if (parMoeda1.symbol.StartsWith("BCHABC"))
                                {
                                    p1 = parMoeda1.symbol.Substring(0, 6);
                                    p2 = parMoeda1.symbol.Substring(6, 3);
                                }
                                else
                                {
                                    p1 = parMoeda1.symbol.Substring(0, 5);
                                    p2 = parMoeda1.symbol.Substring(5, 4);
                                }
                            }
                            else if (parMoeda1.symbol.Length == 10)
                            {
                                p1 = parMoeda1.symbol.Substring(0, 6);
                                p2 = parMoeda1.symbol.Substring(6, 4);
                            }

                            var parMoedaBTC = bnb.First(x => x.symbol.Contains(m1 == p1 ? p2 : p1 + "BTC"));
                            if (parMoedaBTC.BidPrice == 0)
                                continue;

                            tSymbol = (excInfo.Symbols.Where(x => x.Symbol == parMoedaBTC.symbol));

                            lotSize3 = findLotSize(tSymbol);

                            price3 = findFilterPrice(tSymbol);

                            if (Convert.ToDouble(lotSize3.MinQty).ToString("0." + new string('#', 20)).Contains("."))
                                decPart = Convert.ToDouble(lotSize3.MinQty).ToString("0." + new string('#', 20)).Substring(lotSize3.MinQty.ToString().IndexOf(".") + 1).Length;
                            else
                                decPart = 0;

                            if (Convert.ToDouble(dsoma2).ToString().Contains(".") &&
                                Convert.ToDouble(dsoma2).ToString().Substring(dsoma2.ToString().IndexOf(".") + 1).Length > decPart)
                                dsoma2 = Convert.ToDecimal(dsoma2.ToString().Substring(0, dsoma2.ToString().IndexOf(".") + 1 + decPart));


                            if (Convert.ToDouble(price3.MinPrice).ToString("0." + new string('#', 20)).Contains("."))
                                decPart = Convert.ToDouble(price3.MinPrice).ToString("0." + new string('#', 20)).Substring(price3.MinPrice.ToString().IndexOf(".") + 1).Length;
                            else
                                decPart = 0;

                            if (parMoedaBTC.BidPrice > 1)
                            {
                                final = Math.Round(dsoma2 / parMoedaBTC.BidPrice, decPart);
                            }
                            else
                            {
                                final = Math.Round(dsoma2 * parMoedaBTC.BidPrice, decPart);
                            }

                            if (dsoma2 < lotSize3.MinQty && dsoma2 > lotSize3.MaxQty)
                            {
                                txbResult.AppendText("Invalide quantity for " + parMoedaBTC.symbol + " MinQty: " + lotSize3.MinQty + " MaxQty: " + lotSize3.MaxQty + "\r\n");
                                continue;
                            }

                            if (parMoedaBTC.askQty <= final)
                                Valid = false;

                            dfinal = final - Math.Round(final * fee,8);

                            if (Convert.ToDouble(dfinal).ToString().Contains("."))
                                dfinal = Convert.ToDecimal(dfinal.ToString().Substring(0, dfinal.ToString().IndexOf(".") + 1 + decPart));

                            txt.AppendLine("Exchange: " + parMoedaBTC.symbol + " - Sell Price: " + parMoedaBTC.BidPrice + p2 + " - Total Coin: " + dfinal + m2);                         

                            var Sp = Math.Round((((dfinal * 100) / startCurrence) - 100), 2);                       
                       
                            if (Sp >= Convert.ToDecimal(txbConGanAbov.Text.Trim()))
                            {
                                var msg = Sp + "% *** 1º Step: " + par.symbol + " - 2º Step: " + parMoeda1.symbol + " - 3º Step: " + parMoedaBTC.symbol;
                                txt.AppendLine(msg);
                                txt.AppendLine("GAIN!!!!!!!!!!! Start: " + startCurrence + " | Stop: "+ final + " | Valid Transaction: " + Valid);
                                txt.AppendLine("==============================================================================");                            
                                txbResult.Text += txt;
                                txbInBTC.Text = Convert.ToString( final);

                            

                                if (Valid && Auto)
                                {
                                    if (Convert.ToDouble(lotSize1.MinQty).ToString("0." + new string('#', 20)).Contains("."))
                                        decPart = Convert.ToDouble(lotSize1.MinQty).ToString("0." + new string('#', 20)).Substring(lotSize1.MinQty.ToString().IndexOf(".") + 1).Length;
                                    else
                                        decPart = 0;

                                    //var createOrder1 = await client.CreateOrder(new CreateOrderRequest()
                                    var createOrder1 = await client.CreateTestOrder(new CreateOrderRequest()                                    
                                    {                                    
                                        Price = par.AskPrice,
                                        Quantity = Math.Round(soma1,decPart),
                                        Side = OrderSide.Buy,
                                        Symbol = par.symbol,
                                        Type = OrderType.LimitMaker,
                                    });
                                    request += 1;

                                    if (Convert.ToDouble(lotSize2.MinQty).ToString("0." + new string('#', 20)).Contains("."))
                                        decPart = Convert.ToDouble(lotSize2.MinQty).ToString("0." + new string('#', 20)).Substring(lotSize2.MinQty.ToString().IndexOf(".") + 1).Length;
                                    else
                                        decPart = 0;

                                    //var createOrder2 = await client.CreateOrder(new CreateOrderRequest()
                                    var createOrder2 = await client.CreateTestOrder(new CreateOrderRequest()
                                    {                                  
                                        Price = parMoeda1.BidPrice,
                                        Quantity = Math.Round(dsoma1, decPart),
                                        Side = OrderSide.Sell,
                                        Symbol = parMoeda1.symbol,
                                        Type = OrderType.LimitMaker,
                                    });
                                    request += 1;

                                    if (Convert.ToDouble(lotSize3.MinQty).ToString("0." + new string('#', 20)).Contains("."))
                                        decPart = Convert.ToDouble(lotSize3.MinQty).ToString("0." + new string('#', 20)).Substring(lotSize3.MinQty.ToString().IndexOf(".") + 1).Length;
                                    else
                                        decPart = 0;

                                    //var createOrder3 = await client.CreateOrder(new CreateOrderRequest()
                                    var createOrder3 = await client.CreateTestOrder(new CreateOrderRequest()
                                    {                                   
                                        Price = parMoedaBTC.BidPrice,
                                        Quantity = Math.Round(dsoma2, decPart),
                                        Side = OrderSide.Sell,
                                        Symbol = parMoedaBTC.symbol,
                                        Type = OrderType.LimitMaker,
                                    });
                                    request += 1;
                                }
                            }

                        }

                    
                    }

                    txbResult.AppendText("Finish...\r\n");

                    if (request > 1190)
                    {
                        txbResult.AppendText("Request Limit Wait!!!\r\n");
                    }

                } while (chbAutoRun.Checked && request <= 1190);

            }
            catch (BinanceBadRequestException ex)
            {
                txbResult.AppendText(ex.ErrorDetails + "\r\n");
            }
            catch (BinanceServerException ex)
            {
                txbResult.AppendText(ex.ErrorDetails + "\r\n");
            }
            catch (BinanceTimeoutException ex)
            {
                txbResult.AppendText(ex.ErrorDetails + "\r\n");
            }
            catch (BinanceException ex)
            {
                txbResult.AppendText(ex.ErrorDetails + "\r\n");
            }
            catch (Exception ex)
            {                
                txbResult.AppendText(ex.Message + "\r\n");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnRun.Enabled = true;
                btnRun.Text = "Run!!";
                timer1.Stop();
                timer1.Dispose();
                timer1 = null;
            }
        }       

        private ExchangeInfoSymbolFilterLotSize findLotSize(IEnumerable<ExchangeInfoSymbol> tsymbol)
        {
            foreach (var lot in tsymbol)
            {
                foreach (var t in lot.Filters)
                {

                    if (t.GetType().Name.Equals("ExchangeInfoSymbolFilterLotSize"))
                    {
                        return (ExchangeInfoSymbolFilterLotSize)t;
                    }

                }               
            }

            return null;
        }

        private ExchangeInfoSymbolFilterPrice findFilterPrice(IEnumerable<ExchangeInfoSymbol> tsymbol)
        {
            foreach (var lot in tsymbol)
            {
                foreach (var t in lot.Filters)
                {

                    if (t.GetType().Name.Equals("ExchangeInfoSymbolFilterPrice"))
                    {
                        return (ExchangeInfoSymbolFilterPrice)t;
                    }

                }
            }

            return null;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Teste();
        }

        private void ChbUseCurrBTC_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUseCurrBTC.Checked)
                txbInBTC.ReadOnly = true;
            else
                txbInBTC.ReadOnly = false;
            
        }

        private void TxbResult_TextChanged(object sender, EventArgs e)
        {
            txbResult.SelectionStart = txbResult.Text.Length;
            txbResult.ScrollToCaret();
            txbResult.Refresh();
        }
    }    

}
