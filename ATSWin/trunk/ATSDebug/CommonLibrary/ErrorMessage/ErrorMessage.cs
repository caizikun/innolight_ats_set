using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS_Framework
{
     public enum Error_Type : byte
    {
        Equpment_IO_Error = 0,
        Test_Methord_Error = 1,
        Equpment_Instantiate_Error = 3,
        Calculate_Error = 2,
        Source_Code_Error=4,
        Parameters_Error=5
    }

     public enum EQUIP_IO_Type : byte
     {
         GPIB = 0,
         USB = 1,
         RS232 = 2
     }

    public class Error_Message : Exception
    {
        public string Str_Test_Mode_name = null;
        public string Str_Test_Mode_Methord = null;

        public Error_Type error_type;
        public EQUIP_IO_Type IO_Type;
        //  public string Str_Error_Type =null;

        public byte byte_Error_Type;
        public byte byte_IO_Type;
        public string Str_Equpment_name = null;

        public string Str_Function = null;

        public int iAddr = 0;

        public int iChannel = 0;

        public string Str_Set_Value = null;

        public string Str_Message = null;

        public string Str_solution = null;

        public Error_Message(string STR_Message)
        {
            Str_Message = STR_Message;
        }

        public Error_Message(Error_Type error_type1, string STR_Message)
        {
            error_type = error_type1;
            Str_Message = STR_Message;
        }

        public Error_Message(Error_Message e)
        {
            Str_Test_Mode_name = e.Str_Test_Mode_name;
            Str_Test_Mode_Methord = e.Str_Test_Mode_Methord;
            IO_Type = EQUIP_IO_Type.GPIB;
            //  Str_Error_Type = e.Str_Error_Type;
            Str_Equpment_name = e.Str_Equpment_name;
            Str_Function = e.Str_Function;

            iAddr = e.iAddr;
            // IO_Type. = e.IO_Type;
            iChannel = e.iChannel;
            Str_Set_Value = e.Str_Set_Value;
            Str_Message = e.Str_Message;
            Str_solution = e.Str_solution;


        }

        public Error_Message(string message, Exception innerException)
        {

        }

        public Error_Message(string Str_Current_Message, string Str_Current_solution)
        {

            Str_Message = Str_Current_Message;
            Str_solution = Str_Current_solution;
        }

        public Error_Message(Error_Type error_type, string Str_Current_Equpment_name, string Str_Current_Funtion, int iCurrent_Addr, EQUIP_IO_Type IO_Type1, int iCurrent_Channel, string Str_Current_Set_Value, string Str_Current_Message, string Str_Current_solution)
        {
            //  Str_Error_Type = Str_Current_Error_Type;
            Str_Equpment_name = Str_Current_Equpment_name;
            Str_Function = Str_Current_Funtion;
            iAddr = iCurrent_Addr;
            IO_Type = IO_Type1;
            iChannel = iCurrent_Channel;
            Str_Set_Value = Str_Current_Set_Value;
            Str_Message = Str_Current_Message;
            Str_solution = Str_Current_solution;

        }

        public Error_Message(string Str_Current_Test_Mode_name, string Str_Current_Test_Mode_Methord, Error_Message e)
        {
            Str_Test_Mode_name = Str_Current_Test_Mode_name;
            Str_Test_Mode_Methord = Str_Current_Test_Mode_Methord;

            //   Str_Error_Type = e.Str_Error_Type;
            Str_Equpment_name = e.Str_Equpment_name;
            Str_Function = e.Str_Function;
            iAddr = e.iAddr;
            byte_IO_Type = e.byte_IO_Type;
            iChannel = e.iChannel;
            Str_Set_Value = e.Str_Set_Value;
            Str_Message = e.Str_Message;
            Str_solution = e.Str_solution;
        }

        public Error_Message(Error_Type error_type1, string Str_Current_Funtion, string Str_Current_Message, string Str_Current_solution)
        {
            error_type = error_type1;
            Str_Function = Str_Current_Funtion;
            Str_Message = Str_Current_Message;
            Str_solution = Str_Current_solution;
        }

        public Error_Message(Error_Type error_type1, string Str_Current_Equpment_name, string Str_Current_Funtion, string Str_Current_Message, string Str_Current_solution)
        {
            Str_Equpment_name = Str_Current_Equpment_name;
            Str_Function = Str_Current_Funtion;
      
            Str_Message = Str_Current_Message;
            Str_solution = Str_Current_solution;
        }
        public Error_Message(Error_Type error_type1, string Str_Current_Equpment_name, string Str_Current_Funtion, int iCurrent_Addr, EQUIP_IO_Type IO_Type1, string Str_Set_Value1, string Str_Current_Message, string Str_Current_solution)
        {
            Str_Equpment_name = Str_Current_Equpment_name;
            Str_Function = Str_Current_Funtion;
            iAddr = iCurrent_Addr;
            IO_Type = IO_Type1;
            Str_Set_Value = Str_Set_Value1;
            Str_Message = Str_Current_Message;
            Str_solution = Str_Current_solution;
        }
    
    }
}
