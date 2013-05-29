using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GsmComm.GsmCommunication;
using GsmComm.Interfaces;
using GsmComm.PduConverter;
using GsmComm.Server;

namespace MinSobo_SMS
{
    class SMS
    {
        public SMS()
        {
        }

        public void SendSMS(string port,string nomer,string message)
        {
            try
            {
                GsmCommMain comm;
                SmsSubmitPdu pdu;
                comm = new GsmCommMain(Convert.ToInt32(port), 115200);
                comm.Open();
                pdu = new SmsSubmitPdu(message, nomer, "");
                comm.SendMessage(pdu);
                comm.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
