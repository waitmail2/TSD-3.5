using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TSD
{
    public  class PowerStatus
    {


        /*Начало: Проверка батареек*/

        //Более старая функция.
        [DllImport("coredll")]
        static public extern uint GetSystemPowerStatusEx(SYSTEM_POWER_STATUS_EX lpSystemPowerStatus, bool fUpdate);

        //Более новая функция (побольше возможностей.
        [DllImport("coredll")]
        static public extern uint GetSystemPowerStatusEx2(SYSTEM_POWER_STATUS_EX2 lpSystemPowerStatus, uint dwLen, bool fUpdate);


        ////Структура новой функции.
        //public class SYSTEM_POWER_STATUS_EX2
        //{
        //    public byte ACLineStatus;
        //    public byte BatteryFlag;
        //    public byte BatteryLifePercent;
        //    public byte Reserved1;
        //    public uint BatteryLifeTime;
        //    public uint BatteryFullLifeTime;
        //    public byte Reserved2;
        //    public byte BackupBatteryFlag;
        //    public byte BackupBatteryLifePercent;
        //    public byte Reserved3;
        //    public uint BackupBatteryLifeTime;
        //    public uint BackupBatteryFullLifeTime;
        //    public uint BatteryVoltage;
        //    public uint BatteryCurrent;
        //    public uint BatteryAverageCurrent;
        //    public uint BatteryAverageInterval;
        //    public uint BatterymAHourConsumed;
        //    public uint BatteryTemperature;
        //    public uint BackupBatteryVoltage;
        //    public byte BatteryChemistry;
        //}

            [DllImport("coredll")]
        static public extern Int32 GetSystemPowerStatusEx2(SYSTEM_POWER_STATUS_EX2 lpSystemPowerStatus, Int32 dwLen, bool fUpdate);
         
        public class SYSTEM_POWER_STATUS_EX2
        {
            public byte ACLineStatus;// BYTE ACLineStatus; - состояние внешнего питания
            public byte BatteryFlag; // BYTE BatteryFlag; - состояние основной батареи
            public byte BatteryLifePercent;//BYTE BatteryLifePercent; - процент зарядки основной батареи
            public byte Reserved1;
            public uint BatteryLifeTime;//DWORD BatteryLifeTime; - время, на которое хватит заряда
            public uint BatteryFullLifeTime;//DWORD BatteryFullLifeTime; - время, на которое хватит полной батареи
            public byte Reserved2;
            public byte BackupBatteryFlag;//BYTE BackupBatteryFlag; - состояние резервной батареи
            public byte BackupBatteryLifePercent;//BYTE BackupBatteryLifePercent; - процент заряда резервной батареи
            public byte Reserved3;
            public uint BackupBatteryLifeTime;//DWORD BackupBatteryLifeTime; - время остатка заряда резервной батареи
            public uint BackupBatteryFullLifeTime;//DWORD BackupBatteryFullLifeTime; - полное время резервной батареи
            //Физические характеристики батареи:
            public uint BatteryVoltage;//DWORD BatteryVoltage; - емкость батреи (mV)
            public uint BatteryCurrent;//DWORD BatteryCurrent; - ток батареи (mA)
            public uint BatteryAverageCurrent;//DWORD BatteryAverageCurrent; - средний ток
            public uint BatteryAverageInterval;//DWORD BatteryAverageInterval; - интервал изменения тока
            public uint BatterymAHourConsumed;//DWORD BatterymAHourConsumed; - емкость батареи в mAH
            public uint BatteryTemperature;//DWORD BatteryTemperature; - температура батареи
            public uint BackupBatteryVoltage;//DWORD BackupBatteryVoltage; - емкость резервной батареи
            public byte BatteryChemistry;//BYTE BatteryChemistry; - тип батареи (LION, NiCD, NIMN… )   
        }
 
       

        //Структура старой функции.
        public class SYSTEM_POWER_STATUS_EX
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifeTime;
            public uint BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifeTime;
            public uint BackupBatteryFullLifeTime;
        }



        public string ReportPowerStatus2(string what) 
        {
         
            SYSTEM_POWER_STATUS_EX2 Status = new SYSTEM_POWER_STATUS_EX2();              
            GetSystemPowerStatusEx2(Status, Marshal.SizeOf(Status), false);

            string result = "";
            try
            {
                string status = string.Empty;

                SYSTEM_POWER_STATUS_EX powerStatus;
                powerStatus = new SYSTEM_POWER_STATUS_EX();

                GetSystemPowerStatusEx(powerStatus, true);

                string battery1 = powerStatus.BatteryLifePercent.ToString();

                string battery2 = powerStatus.BackupBatteryLifePercent.ToString();

                //Если передан параметр main - то основная батарея иначе backup батарея.
                if (what == "main")
                {
                    result = battery1;
                }
                else
                {
                    result = battery2;
                }

            }
            catch
            {

            }
            return result;

        }

        //Непосредственно - Функция получения информациии о заряде батареек.
        //Буду использовать старубю функцию.
        //protected virtual string ReportPowerStatus(string what)
        public string ReportPowerStatus(string what)
        {
            string result = "";
            try
            {
                string status = string.Empty;

                SYSTEM_POWER_STATUS_EX powerStatus;
                powerStatus = new SYSTEM_POWER_STATUS_EX();

                GetSystemPowerStatusEx(powerStatus, true);



                string battery1 = powerStatus.BatteryLifePercent.ToString();

                string battery2 = powerStatus.BackupBatteryLifePercent.ToString();

                //Если передан параметр main - то основная батарея иначе backup батарея.
                if (what == "main")
                {
                    result = battery1;
                }
                else
                {
                    result = battery2;
                }

            }
            catch
            {

            }
            return result;
        }



        /*Конец: Проверка батареек*/

    }
}
