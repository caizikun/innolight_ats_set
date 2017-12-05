using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using ATS_Framework;
namespace ATS_Driver
{
   public class EDVT:DUT
    {
       public Algorithm algorithm = new Algorithm();
       public EDVT(logManager logmanager)
        {
            logger = logmanager;
        }


       public override bool Initialize(TestModeEquipmentParameters[] EDVTlist)
       {
           try
           {

               if (!Connect()) return false;
           }
           catch (Error_Message error)
           {

               logger.AdapterLogString(3, error.ToString());
               return false;
           }
           return true;
       }
       public override bool Connect()
       {
           
               try
               {
                   MyIO = new IOPort("USB", deviceIndex.ToString());
                   EquipmentConnectflag = true;
                   return EquipmentConnectflag;
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;

               }
       }
       public override bool ChangeChannel(string channel)
       {
           return true;
       }
       public override bool configoffset(string channel, string offset)
       {
           return true;
       }
       public override bool Configure()
       {
           return true;
       }
       //read/writereg
       public override byte[] ReadReg(int deviceAddress, int regAddress, int readLength)
       {
           return USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, readLength);

       }
       public override byte[] WrtieReg(int deviceAddress, int regAddress, byte[] dataToWrite)
       {
           return USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
       }
       public override byte[] FpgaIICRead(int regAddress, int readLength)
       {
           try
           {

               return MyIO.FpgaIICRead(deviceIndex, regAddress, 0xC2, readLength);
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return new byte[16];
           }
       
       }
       public override byte[] FpgaIICWrtie(int regAddress, byte[] dataToWrite)
       {
           try
           {

               return MyIO.FpgaIICWrtie(deviceIndex, regAddress, 0xC2, dataToWrite);
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return new byte[16];
           }

       }
       public override bool FpgaInrush( byte operate)
       {
           // 0 tx,1 rx
         try
          {

              MyIO.FpgaInrush(deviceIndex, false, 0xC4, operate);
                     return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
       }

       public override bool FpgaLPMode(byte operate)
       {//0 off,1 on

           
           try
           {

               MyIO.FpgaInrush(deviceIndex, false, 0xC5, operate);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }

     
       public override byte[] MDIORead(int regAddress, int readLength)
       {
           try
           {

               return MyIO.FpgaIICRead(deviceIndex, regAddress, 0xC1, readLength);
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return new byte[16];
           }

       }
       public override byte[] MDIOWrtie(int regAddress, byte[] dataToWrite)
       {
           try
           {

               return MyIO.FpgaIICWrtie(deviceIndex, regAddress, 0xC1, dataToWrite);
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return new byte[16];
           }

       }
       public override bool FpgaReset(byte operate)
       {//0 reset,1 release reset
           try
           {

               MyIO.FpgaInrush(deviceIndex, false, 0xC7, operate);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }

       public override bool FpgaResetTimeSet(double delaytime)
       {
           try
           {

               MyIO.FpgaResetTimeSet(deviceIndex, false, 0xCC, delaytime);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }

       public override bool FpgaTxDisable(byte operate)
       {//0 off,1 on
           try
           {

               MyIO.FpgaInrush(deviceIndex, false, 0xC3, operate);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }
       public override bool McuFpgaModeSel(byte operate, byte ICSelect)
       {//0 off,1 on
           //icsel  0 fpga  1 mcu
           try
           {

               MyIO.McuFpgaModeSel(deviceIndex, false, 0xC3, operate, ICSelect);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }
       public override bool FpgaNorsineTxRx( byte operate, int acamplitude, double acfreq, double dcamplitude)
       {//operate 
           //0 nonoise 1 addnoise  2 turnoffdc
           byte bop;
           try
           {
               switch (operate)
               {
                   case 0:
                       bop = 0x38;
                       break;
                   case 1:
                       bop =0x06;
                       break;
                   case 2:
                       bop = 0x3e;
                       break;
                   default:
                       bop = 0x38;
                       break;
               }

               MyIO.FpgaNorsineTxRx(deviceIndex, false, 0xC8, bop, acamplitude,acfreq,dcamplitude);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }

       public override bool SamplePowerCurrent(out double current,out double volt)
       {
           byte[] temp = new byte[4];
           current = 0;
           volt = 0;
           try
           {

             temp =  MyIO.SamplePowerCurrent(deviceIndex, false, 4);
             current = (temp[0] * 256 + temp[1]) * 0.00487;
             volt = (temp[2] * 256 + temp[3]) * 18;

               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }

       public override bool LoadSwitch( byte operate, int acamplitude, double acfreq, double dcamplitude)
       {//operate
           //normal-work   0
            //inrush tx   1
            //inrush  rx  2
            //add sine  tx  3
            //add sine  rx  4
            //add sine  all  5
            //off power   6
           byte bop;
           try
           {
               switch (operate)
               {
                   case 0:
                       bop = 0x0f;
                       break;
                   case 1:
                       bop = 0x02;
                       break;
                   case 2:
                       bop = 0x00;
                       break;
                   case 3:
                       bop = 0x33;
                       break;
                   case 4:
                       bop = 0x53;
                       break;
                   case 5:
                       bop = 0x73;
                       break;
                   case 6:
                       bop = 0x03;
                       break;
                   default:
                       bop = 0x0f;
                       break;
               }
               if ((bop == 1) || (bop == 2))
               {
                   MyIO.FpgaInrush(deviceIndex, false, 0xC4, bop);
               }
               else
               {
                   MyIO.FpgaNorsineTxRx(deviceIndex, false, 0xC8, bop, acamplitude, acfreq, dcamplitude);
               
               }
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }

       public override bool FpgaSwitch(byte operate)
       {
           //operate
           //0 TX1,1 TX2,2 TX3,3 TX4,4 RX1,5 RX2,6 RX3,7 RX4,8 POWER,9 INT,10 MODE PRSL,11 MODE SEL,12 RESET,13 LP MODE,14 SCL ,15 SDA
           byte oph, opl;
           switch (operate)
           {
               case 0:
                   oph = 0x0f;
                   opl = 0x09;
                   break;
               case 1:
                   oph = 0x0f;
                   opl = 0x0b;
                   break;
               case 2:
                   oph = 0x0f;
                   opl = 0x0f;
                   break;
               case 3:
                   oph = 0x0f;
                   opl = 0x0d;
                   break;
               case 4:
                   oph = 0x0f;
                   opl = 0x12;
                   break;
               case 5:
                   oph = 0x0f;
                   opl = 0x14;
                   break;
               case 6:
                   oph = 0x0f;
                   opl = 0x10;
                   break;
               case 7:
                   oph = 0x0f;
                   opl = 0x16;
                   break;
               case 8:
                   oph = 0x0f;
                   opl = 0xc0;
                   break;
               case 9:
                   oph = 0x0f;
                   opl = 0x00;
                   break;
               case 10:
                   oph = 0x0f;
                   opl = 0x20;
                   break;
               case 11:
                   oph = 0x0f;
                   opl = 0x40;
                   break;
               case 12:
                   oph = 0x0f;
                   opl = 0x60;
                   break;
               case 13:
                   oph = 0x0f;
                   opl = 0x80;
                   break;
               case 14:
                   oph = 0x0f;
                   opl = 0xa0;
                   break;
               case 15:
                   oph = 0x0f;
                   opl = 0xe0;
                   break;
               default:
                    oph = 0x0f;
                   opl = 0x09;
                   break;
           }
           try
           {

               MyIO.FpgaSwitch(deviceIndex, false, 0xCA, oph,opl);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }

   
   }
}
