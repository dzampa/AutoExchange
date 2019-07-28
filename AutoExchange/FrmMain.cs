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
        public AutoExchange()
        {
            InitializeComponent();
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
            string p1;
            string p2;
            long timestamp1;
            long timestamp2;

            bool Valid;
            bool Auto;

            try
            {

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

                //Initialise the general client client with config
                var client = new BinanceClient(new ClientConfiguration()
                {
                    ApiKey = apiKey,
                    SecretKey = secretKey,
                    Logger = ProgramLogger,
                });

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

                timestamp2 = serverTime.ServerTime.ConvertToUnixTime();

                client.TimestampOffset = System.TimeSpan.FromMilliseconds(timestamp2-timestamp1);

                if (chbUseCurrBTC.Checked)
                {
                    txbResult.AppendText("Get funds from BTC Funds account (Online)...\r\n");

                    // Get account information
                    var accountInformation = await client.GetAccountInformation(3500);

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

                var bnb = await client.GetDailyTicker24();

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

                    if (par.AskPrice == 0)
                        continue;

                    soma1 = Math.Round(startCurrence / par.AskPrice, 8);
                    if (par.askQty <= soma1)
                        Valid = false;
                    soma1 -= Math.Round(soma1 * fee,8);                    

                    var pares = bnb.Where(x => x.symbol.StartsWith(m1) && !x.symbol.Contains(par.symbol) && !x.symbol.Contains("USD")).ToList();

                    foreach (var parMoeda1 in pares)
                    {
                        txt = new StringBuilder();
                        txt.AppendLine("==============================================================================");
                        txt.AppendLine("Exchange: " + par.symbol + " - Buy Price: " + par.AskPrice + m2 + " - Total Coin: " + soma1 + m1);

                        soma2 = 0;
                        if (par.AskPrice == 0)
                            continue;
                        if (parMoeda1.AskPrice > 1)
                        {
                            soma2 = Math.Round(soma1 / parMoeda1.AskPrice, 8);
                        }
                        else
                        {
                            soma2 = Math.Round(soma1 * parMoeda1.AskPrice, 8);
                        }

                        if (parMoeda1.askQty <= soma2)
                            Valid = false;

                        soma2 -= Math.Round(soma2 * fee ,8);
                        txt.AppendLine("Exchange: " + parMoeda1.symbol + " - Buy Price: " + parMoeda1.BidPrice + m1 + " - Total Coin: " + soma2 + parMoeda1.symbol.Replace(m1,""));

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
                        if (parMoedaBTC.AskPrice == 0)
                            continue;
                        
                        var final = Math.Round(soma2 * parMoedaBTC.AskPrice, 8);
                        if (parMoedaBTC.askQty <= final)
                            Valid = false;
                        final -= Math.Round(final * fee,8);

                        txt.AppendLine("Exchange: " + parMoedaBTC.symbol + " - Sell Price: " + parMoedaBTC.BidPrice + p2 + " - Total Coin: " + final + m2);
                        var Sp = Math.Round((((final * 100) / startCurrence) - 100), 2);
                        if (final >= startCurrence && Sp >= Convert.ToDecimal(txbConGanAbov.Text.Trim()))
                        {
                            var msg = Sp + "% *** 1º Step: " + par.symbol + " - 2º Step: " + parMoeda1.symbol + " - 3º Step: " + parMoedaBTC.symbol;
                            txt.AppendLine(msg);
                            txt.AppendLine("GAIN!!!!!!!!!!! Start: " + startCurrence + " | Stop: "+ final + " | Valid Transaction: " + Valid);
                            txt.AppendLine("==============================================================================");                            
                            txbResult.Text += txt;
                            txbInBTC.Text = Convert.ToString( final); 
                            if (Valid && Auto)
                            {
                                var createOrder1 = await client.CreateTestOrder(new CreateOrderRequest()
                                {                                    
                                    Price = par.AskPrice,
                                    Quantity = Math.Round(soma1, 8),
                                    Side = OrderSide.Buy,
                                    Symbol = par.symbol,
                                    Type = OrderType.LimitMaker,
                                });


                               var createOrder2= await client.CreateTestOrder(new CreateOrderRequest()
                               {                                  
                                   Price = parMoeda1.BidPrice,
                                   Quantity = Math.Round(soma1, 8),
                                   Side = OrderSide.Sell,
                                   Symbol = parMoeda1.symbol,
                                   Type = OrderType.LimitMaker,
                               });

                               var createOrder3 = await client.CreateTestOrder(new CreateOrderRequest()
                               {                                   
                                   Price = parMoedaBTC.BidPrice,
                                   Quantity = Math.Round(soma2, 8),
                                   Side = OrderSide.Sell,
                                   Symbol = parMoedaBTC.symbol,
                                   Type = OrderType.LimitMaker,
                               });
                            }
                        }

                    }

                    
                }
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
        }       

        private void btnRun_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txbResult.Text = "";
            Teste();
            Cursor.Current = Cursors.Default;
        }

        private void ChbUseCurrBTC_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUseCurrBTC.Checked)
                txbInBTC.Enabled = false;
            else
                txbInBTC.Enabled = true;
            
        }

        private void TxbResult_TextChanged(object sender, EventArgs e)
        {
            txbResult.SelectionStart = txbResult.Text.Length;
            txbResult.ScrollToCaret();
            txbResult.Refresh();
        }
    }    

}
