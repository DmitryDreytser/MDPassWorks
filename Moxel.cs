using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace MDPassWorks
{
    public class Moxel
    {

        public enum FontWeight : int
        {
            FW_DONTCARE = 0,
            FW_THIN = 100,
            FW_EXTRALIGHT = 200,
            FW_LIGHT = 300,
            FW_NORMAL = 400,
            FW_MEDIUM = 500,
            FW_SEMIBOLD = 600,
            FW_BOLD = 700,
            FW_EXTRABOLD = 800,
            FW_HEAVY = 900,
        }
        public enum FontCharSet : byte
        {
            ANSI_CHARSET = 0,
            DEFAULT_CHARSET = 1,
            SYMBOL_CHARSET = 2,
            SHIFTJIS_CHARSET = 128,
            HANGEUL_CHARSET = 129,
            HANGUL_CHARSET = 129,
            GB2312_CHARSET = 134,
            CHINESEBIG5_CHARSET = 136,
            OEM_CHARSET = 255,
            JOHAB_CHARSET = 130,
            HEBREW_CHARSET = 177,
            ARABIC_CHARSET = 178,
            GREEK_CHARSET = 161,
            TURKISH_CHARSET = 162,
            VIETNAMESE_CHARSET = 163,
            THAI_CHARSET = 222,
            EASTEUROPE_CHARSET = 238,
            RUSSIAN_CHARSET = 204,
            MAC_CHARSET = 77,
            BALTIC_CHARSET = 186,
        }
        public enum FontPrecision : byte
        {
            OUT_DEFAULT_PRECIS = 0,
            OUT_STRING_PRECIS = 1,
            OUT_CHARACTER_PRECIS = 2,
            OUT_STROKE_PRECIS = 3,
            OUT_TT_PRECIS = 4,
            OUT_DEVICE_PRECIS = 5,
            OUT_RASTER_PRECIS = 6,
            OUT_TT_ONLY_PRECIS = 7,
            OUT_OUTLINE_PRECIS = 8,
            OUT_SCREEN_OUTLINE_PRECIS = 9,
            OUT_PS_ONLY_PRECIS = 10,
        }
        public enum FontClipPrecision : byte
        {
            CLIP_DEFAULT_PRECIS = 0,
            CLIP_CHARACTER_PRECIS = 1,
            CLIP_STROKE_PRECIS = 2,
            CLIP_MASK = 0xf,
            CLIP_LH_ANGLES = (1 << 4),
            CLIP_TT_ALWAYS = (2 << 4),
            CLIP_DFA_DISABLE = (4 << 4),
            CLIP_EMBEDDED = (8 << 4),
        }
        public enum FontQuality : byte
        {
            DEFAULT_QUALITY = 0,
            DRAFT_QUALITY = 1,
            PROOF_QUALITY = 2,
            NONANTIALIASED_QUALITY = 3,
            ANTIALIASED_QUALITY = 4,
            CLEARTYPE_QUALITY = 5,
            CLEARTYPE_NATURAL_QUALITY = 6,
        }
        [Flags]
        public enum FontPitchAndFamily : byte
        {
            DEFAULT_PITCH = 0,
            FIXED_PITCH = 1,
            VARIABLE_PITCH = 2,
            FF_DONTCARE = (0 << 4),
            FF_ROMAN = (1 << 4),
            FF_SWISS = (2 << 4),
            FF_MODERN = (3 << 4),
            FF_SCRIPT = (4 << 4),
            FF_DECORATIVE = (5 << 4),
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public FontWeight lfWeight;
            [MarshalAs(UnmanagedType.U1)]
            public bool lfItalic;
            [MarshalAs(UnmanagedType.U1)]
            public bool lfUnderline;
            [MarshalAs(UnmanagedType.U1)]
            public bool lfStrikeOut;
            public FontCharSet lfCharSet;
            public FontPrecision lfOutPrecision;
            public FontClipPrecision lfClipPrecision;
            public FontQuality lfQuality;
            public FontPitchAndFamily lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName;

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("LOGFONT\n");
                sb.AppendFormat("   lfHeight: {0}\n", lfHeight);
                sb.AppendFormat("   lfWidth: {0}\n", lfWidth);
                sb.AppendFormat("   lfEscapement: {0}\n", lfEscapement);
                sb.AppendFormat("   lfOrientation: {0}\n", lfOrientation);
                sb.AppendFormat("   lfWeight: {0}\n", lfWeight);
                sb.AppendFormat("   lfItalic: {0}\n", lfItalic);
                sb.AppendFormat("   lfUnderline: {0}\n", lfUnderline);
                sb.AppendFormat("   lfStrikeOut: {0}\n", lfStrikeOut);
                sb.AppendFormat("   lfCharSet: {0}\n", lfCharSet);
                sb.AppendFormat("   lfOutPrecision: {0}\n", lfOutPrecision);
                sb.AppendFormat("   lfClipPrecision: {0}\n", lfClipPrecision);
                sb.AppendFormat("   lfQuality: {0}\n", lfQuality);
                sb.AppendFormat("   lfPitchAndFamily: {0}\n", lfPitchAndFamily);
                sb.AppendFormat("   lfFaceName: {0}\n", lfFaceName);

                return sb.ToString();
            }
        }

        static int GetNumberArray(byte[] buf, out int[] pArray, ref int nPos, int nMult = 1)
        {
            int nArrayCount = 0;
            pArray = null;
            nArrayCount = BitConverter.ToInt32(buf, nPos);
           
            if (nArrayCount > 0x10000)
            {
                nArrayCount = BitConverter.ToInt16(buf, nPos);
                nPos += 2;
            }
            else
                nPos += 4;
            

            if (nArrayCount > 0)
            {
            //    if (nArrayCount > 10000)
            //        throw new Exception("Ошибка загрузки размера массива!");

                nArrayCount = nArrayCount * nMult;
                pArray = new int[nArrayCount];

                //загружаем список номеров колонок
                for (int i = 0; i < nArrayCount; i++)
                {
                    if (nPos > buf.Length - 4)
                        return nArrayCount;
                    //nPos += 2;
                    pArray[i] = BitConverter.ToInt32(buf, nPos);
                    pArray[i]++;
                    nPos += 4;
                }
            }
            //nPos += 4;
            return nArrayCount;
        }

        static void GetText(byte[] buf, out string Str, ref int nPos, bool bBig = true)
        {
            Str = string.Empty;
            int nLength = 0;
            if (buf[nPos] == 0xFF)//bBig&&
            {
                nPos++;
                nLength = BitConverter.ToInt16(buf, nPos);
                nPos += 2;
            }
            else
            {
                nLength = buf[nPos];
                nPos++;
            }

            if (nLength < 0 || nLength > 0xFFFF)
                throw new Exception("Ошибка загрузки длины текста!");

            Str = Encoding.GetEncoding(1251).GetString(buf, nPos, nLength);
            nPos += nLength;
        }

        public class CCell
        {
            public int Version;
            public int nColumn;		//номер колонок
            public string csText;		//текст ячейки
            public string csFormula;	//расшифровка

            public string csFormat;
            public string csMaska;

            public byte nType;		//тип данных ячейки U(неопределенный),N(число),S(строка),B(справчоник),O(документ),T(счет),K(вид субконто)
            public int nLen;		//длина
            public int nPrec;		//точность
            public bool bProtect;	//флаг защиты ячейки
            public int nControl;	//Контроль (0-Авто,1-обрезать,2-забивать...)
            public int nHPosition;	//Положение по горизонтали (0-лево,2-право,4-по ширине,6-центр) старшая половина слова - количество столбцов, по которому выравнивается ячейка
            public int nVPosition;	//Положение по вертикали (0-верх,8-низ,0x18-центр)
            //Рамка:
            public int nRamkaColor;//цвет рамки (0-55 из палитры)
            public int nRamkaL;//слева
            public int nRamkaU;//сверху
            public int nRamkaR;//справа
            public int nRamkaD;//снизу
            //значения - стиль:
            //1-точки1
            //8-точки2
            //2-тонкая линия
            //3-толстая линия
            //4-очень толстая линия
            //5-двойная линия
            //6-пунктир1
            //7-пунктир2
            //9-толстый пунктир

            public int nBackgroundColor;//цвет фона (0-55 из палитры)
            public int nUzor;//Узор (0-15)
            public int nUzorColor;//цвет узора (0-55 из палитры)


            public int nFontNumber;//номер шрифта из списка
            public int nBold;//жирность
            public int nItalic;//наклонность
            public int nUnderLine;//подчеркивание
            public int nFontColor;//цвет шрифта
            public int nFontHeight;//размер шрифта


            public CCell()
            {
                nColumn = 0;
                nType = 0;
                nLen = 0;
                nPrec = 0;
                bProtect = false;
                nControl = 0;
                nHPosition = 0;
                nVPosition = 0;

                nRamkaColor = 0;
                nRamkaL = 0;
                nRamkaU = 0;
                nRamkaR = 0;
                nRamkaD = 0;
                nBackgroundColor = 0;
                nUzor = 0;
                nUzorColor = 0;

                nFontNumber = 0;
                nBold = 0;
                nItalic = 0;
                nUnderLine = 0;
                nFontColor = 0;
                nFontHeight = 0;
            }
        };

        public class CRow
        {
            public int nColumnCount;//число заполненных колонок
            public CCell[] aCell;
            public int nRowHeight;//высота строки
            //public void Load(byte[] buf, int nPos);
            public int Version;

            public CRow()
            {
                nColumnCount = 0;
                aCell = null;
                nRowHeight = 0;
            }

            public void Load(byte[] buf, int nPos)
            {
                int[] aColumnNumber;

                nColumnCount = GetNumberArray(buf, out aColumnNumber, ref nPos);
                nPos += 2;
                if (nColumnCount > 0)
                {

                    aCell = new CCell[nColumnCount];

                    //загружаем данные колонок
                    for (int i = 0; i < nColumnCount; i++)
                    {
                        aCell[i] = new CCell();
                        aCell[i].Version = Version;
                        aCell[i].nColumn = aColumnNumber[i];

                        //byte pArray[100];
                        //memcpy(pArray,&buf[nPos],sizeof(pArray));

                        byte mode = buf[nPos + 2];
                        byte flag = buf[nPos + 3];

                        bool bExtend = false;// (mode & 0x08) != 0 || (mode & 0x04) != 0;//расш. формат
                        bool bFormula = (flag & 0x40) != 0;//Расшифровка или Формула
                        bool bText =    (flag & 0x80) != 0; //Текст

                        //Рамка:
                        aCell[i].nRamkaColor = buf[nPos + 23];//цвет (0-55 из палитры)
                        aCell[i].nRamkaL = buf[nPos + 18];//слева
                        aCell[i].nRamkaU = buf[nPos + 19];//сверху
                        aCell[i].nRamkaR = buf[nPos + 20];//справа
                        aCell[i].nRamkaD = buf[nPos + 21];//снизу

                        //Узор:
                        aCell[i].nBackgroundColor = buf[nPos + 25];//цвет (0-55 из палитры)
                        aCell[i].nUzor = buf[nPos + 17];//Узор (0-15)
                        aCell[i].nUzorColor = buf[nPos + 22];//цвет узора (0-55 из палитры)

                        aCell[i].nHPosition = buf[nPos + 15];//Положение по горизонтали (0-лево,2-право,4-по ширине,6-центр) старшая половина слова - количество столбцов, по которому выравнивается ячейка
                        aCell[i].nHPosition = aCell[i].nHPosition & 31;
                        aCell[i].nVPosition = buf[nPos + 16];//Положение по вертикали (0-верх,8-низ,0x18-центр)
                        aCell[i].nControl = buf[nPos + 26];	//Контроль (0-Авто,1-обрезать,2-забивать...)
                        aCell[i].bProtect = buf[nPos + 28] == 0;	//флаг Защиты
                        aCell[i].bProtect = !aCell[i].bProtect;

                        aCell[i].nFontNumber = buf[nPos + 8];//номер шрифта из списка шрифтов
                        aCell[i].nBold = buf[nPos + 12];//жирность
                        //4-не жирн
                        //7-жирн
                        if (aCell[i].nBold > 4)
                            aCell[i].nBold = 1;
                        else
                            aCell[i].nBold = 0;

                        aCell[i].nItalic = buf[nPos + 13];//наклонность
                        aCell[i].nUnderLine = buf[nPos + 14];//подчеркивание
                        aCell[i].nFontColor = buf[nPos + 24];//цвет шрифта
                        short nFontH = BitConverter.ToInt16(buf, nPos + 10);
                        //			memcpy(&nFontH,&buf[nPos+10],2);
                        nFontH = (short)(-nFontH / 4);
                        aCell[i].nFontHeight = nFontH;

                        if(Version == 6)
                            nPos += 0x20 - 2;
                        else
                            nPos += 0x20;

                        if (bText)//текст
                            GetText(buf, out aCell[i].csText, ref nPos);
                        if (bFormula)//Формула (расшифровка)
                            GetText(buf, out aCell[i].csFormula, ref nPos);

                        aCell[i].nType = 0;
                        if (bExtend)
                        {
                            aCell[i].nType = buf[nPos + 6]; //U(неопределенный),N(число),S(строка),B(справчоник),O(документ),T(счет),K(вид субконто)
                            aCell[i].nLen = buf[nPos + 7];
                            aCell[i].nPrec = buf[nPos + 11];

                            //длина нижней части
                            int nLength = BitConverter.ToInt16(buf, nPos); ;
                            //memcpy(&nLength,&buf[nPos],2);
                            nPos += 2;

                            int nPos2 = nPos;//независимое позиционирование
                            nPos2 += 0x10 + 3;
                            GetText(buf, out aCell[i].csFormat, ref nPos2);//формат
                            nPos2 += 6;
                            GetText(buf, out aCell[i].csMaska, ref nPos2);//маска

                            nPos += nLength;
                        }

                    }
                    //delete []aColumnNumber;
                }
            }
        };

        public class CSection
        {
            public string csName;
            public int nRange1;
            public int nRange2;
        };

        public Moxel()
        {
            nAllColumnCount = 0;
            nAllRowCount = 0;
            nAllObjectsCount = 0;

            nRowCount = 0;
            aRowNumber = null;
            pRow = null;
            aWidth = null;
            aWidthNumber = null;
            nWidthCount = 0;

            nFontCount = 0;
            aFontNumber = null;
            aFont = null;

            nMergeCells = 0;
            aMergeCells = null;
            VertSection = new Dictionary<string, CSection>();
            HorizSection = new Dictionary<string, CSection>();
        }

        public void Load(byte[] buf)
        {
            int nPos = 0xb;

            Version = buf[nPos];

            //загружаем
            nPos = 0xd;
            nAllColumnCount = BitConverter.ToInt32(buf, nPos);

            nPos = 0x11;
            nAllRowCount = BitConverter.ToInt32(buf, nPos);
            //memcpy(&nAllColumnCount, &buf[nPos], 8);//количество колонок и строк всего
            
            nPos = 0x15;
            nAllObjectsCount =  BitConverter.ToInt32(buf, nPos);

            nPos = 0x37;
            //шрифты
            nFontCount = GetNumberArray(buf, out aFontNumber, ref nPos);
            nPos += 2;
            if (nFontCount > 0)
            {
                aFont = new string[nFontCount];

                for (int i = 0; i < nFontCount; i++)
                {
                    nPos += 28;
                    int nLength = 0x20;
                    aFont[i] = Encoding.GetEncoding(1251).GetString(buf, nPos, nLength);
                    //memcpy(aFont[i].GetBuffer(nLength), &buf[nPos], nLength);
                    //aFont[i].ReleaseBuffer();
                    nPos += nLength;
                }
            }
            nPos += 0x40;

            //ширина столбцов
            nWidthCount = GetNumberArray(buf, out aWidthNumber, ref nPos);
            //nPos += 2;
            if (nWidthCount > 0)
            {
                aWidth = new int[nWidthCount];
                for (int i = 0; i < nWidthCount; i++)
                {
                    nPos += 6;
                    aWidth[i] = BitConverter.ToInt32(buf, nPos);
                    nPos += 24;
                }
            }

            //строки
            nRowCount = GetNumberArray(buf, out aRowNumber, ref nPos);
            nPos += 2;
            if (nRowCount > 0)
            {
                pRow = new CRow[nRowCount];
                
                for (int i = 0; i < nRowCount -1; i++)
                {
                    //byte pArray[100];
                    //memcpy(pArray,&buf[nPos],sizeof(pArray));
                    pRow[i] = new CRow();
                    pRow[i].Version = Version;
                    pRow[i].nRowHeight = BitConverter.ToInt16(buf, nPos + 4);
                    //memcpy(&pRow[i].nRowHeight, &buf[nPos + 4], 2);
                    pRow[i].nRowHeight = pRow[i].nRowHeight / 3;
                    nPos += 0x20;
                    pRow[i].Load(buf, nPos);
                }
            }


            //byte pArray[100];
            //buf[nPos] - количество доп. объектов типа Прямоугольник, Картинка, Диаграмма, OLE-объект
            //загрузка доп объектов

            int nCount = BitConverter.ToInt16(buf, nPos); 
            //memcpy(&nCount, &buf[nPos], 2);

            nPos += 2;

            int nOLEDelta = 18; //дельта только для первого ОЛЕ объекта (почему не знаю, но это так)
            for (int i = 0; i < nCount; i++)
            {
                //memcpy(pArray,&buf[nPos],sizeof(pArray));

                int nType = buf[nPos + 2];//тип объекта
                //0-простой объект (линия, квадрат, блок с текстом)
                //8-сложный объект (картинка, диаграмма 1С, ОЛЕ обьект)

                bool bText = (buf[nPos + 3] & 0x80) == 1; //признак блока с текстом
                bool bFormula = (buf[nPos + 3] & 0x40) == 1; //признак блока с расшифровкой

                //if(bText)
                //    bText = 1;
                //if(bFormula)
                //    bFormula = 1;


                nPos += 30;
                string csText;
                if (bText)
                    GetText(buf, out csText, ref nPos);//текст
                if (bFormula)
                    GetText(buf, out csText, ref nPos);//расшифровка

                int nKind = buf[nPos];
                //вид объекта
                //1-линия
                //2-квадрат
                //3-блок текста (но без текста)
                //4-ОЛЕ обьект (в т.ч. диаграмма 1С)
                //5-картинка

                if (nKind <= 3 || nType == 0)
                    nPos += 40;
                else

                    if (nKind == 4)//ОЛЕ объект
                    {
                        int nLength = BitConverter.ToInt32(buf, nPos + 60 + nOLEDelta);
                        //			memcpy(&nLength,&buf[nPos+60+nOLEDelta],4);
                        nPos += 64 + nOLEDelta + nLength;
                        nOLEDelta = 0;
                    }
                    else//картинка
                        if (nKind == 5)
                        {
                            int nPictureType = buf[nPos + 16];//тип картинки
                            int nLength = BitConverter.ToInt32(buf, nPos + 44);
                            nPos += nLength + 48;
                        }
                        //else//неизвестный объект
                        //{
                        //    string Str;
                        //    Str = string.Format("Встретился неизвестный объект N {0d}", nKind);
                        //    throw new Exception(Str);
                        //    //return;
                        //}

            }

            //buf[nPos] - количество объединенных ячеек
            nCount = BitConverter.ToInt16(buf, nPos); ;
            //memcpy(&nCount, &buf[nPos], 2);
            //загружаем объединенные ячейки
            nMergeCells = GetNumberArray(buf, out aMergeCells, ref nPos, 4) / 4;

            //загружаем секции:
            //вертикальные
            //memcpy(pArray,&buf[nPos],sizeof(pArray));
            nCount = BitConverter.ToInt16(buf, nPos);
            //memcpy(&nCount, &buf[nPos], 2);
            nPos += 2;
            for (int i = 0; i < nCount; i++)
            {
                CSection data = new CSection();
                data.nRange1 = BitConverter.ToInt32(buf, nPos);
                nPos += 4;
                data.nRange2 = BitConverter.ToInt32(buf, nPos); ;
                nPos += 4;
                nPos += 4;
                GetText(buf, out data.csName, ref nPos);
                VertSection.Add(data.csName, data);
            }

            nCount = BitConverter.ToInt16(buf, nPos);
            nPos += 2;
            for (int i = 0; i < nCount; i++)
            {
                CSection data = new CSection();
                data.nRange1 = BitConverter.ToInt32(buf, nPos);
                nPos += 4;
                data.nRange2 = BitConverter.ToInt32(buf, nPos); ;
                nPos += 4;
                nPos += 4;
                GetText(buf, out data.csName, ref nPos);
                HorizSection.Add(data.csName, data);
            }

        }

        int Version = 6;
        public int nAllColumnCount;//всего колонок в таблице
        public int nAllRowCount;//всего строк в таблице
        public int nAllObjectsCount;//всего строк в таблице

        public int nRowCount;//число заполненных строк
        public int[] aRowNumber;//номера строк
        public CRow[] pRow;//строки

        public int nWidthCount;//размер массива задания ширины колонок
        public int[] aWidthNumber;//номера колонок у которых задается ширина
        public int[] aWidth;//ширина колонок

        public int nFontCount;//количество шрифтов
        public int[] aFontNumber;//номера шрифтов
        public string[] aFont;//шрифты

        public int nMergeCells;//количество объединенных ячеек
        public int[] aMergeCells;//массив объединенных ячеек

        //секции:
        public Dictionary<string, CSection> HorizSection;
        public Dictionary<string, CSection> VertSection;

        public static readonly uint[] a1CPallete =
	        {
        //		0xRRGGBB,
		        0x000000,
		        0xFFFFFF,
		        0xFF0000,
		        0x00FF00,
		        0x0000FF,
		        0xFFFF00,
		        0xFF00FF,
		        0x00FFFF,

		        0x800000,
		        0x008000,
		        0x808000,
		        0x000080,
		        0x800080,
		        0x008080,
		        0x808080,
		        0xC0C0C0,

		        0x8080FF,
		        0x802060,
		        0xFFFFC0,
		        0xA0E0E0,
		        0x600080,
		        0xFF8080,
		        0x0080C0,
		        0xC0C0FF,

		        0x00CFFF,
		        0x69FFFF,
		        0xE0FFED,
		        0xDD9CB3,
		        0xB38FEE,
		        0x2A6FF9,
		        0x3FB8CD,
		        0x488436,

		        0x958C41,
		        0x8E5E42,
		        0xA0627A,
		        0x624FAC,
		        0x1D2FBE,
		        0x286676,
		        0x004500,
		        0x453E01,

		        0x6A2813,
		        0x85396A,
		        0x4A3285,
		        0xC0DCC0,
		        0xA6CAF0,
		        0x800000,
		        0x008000,
		        0x000080,

		        0x808000,
		        0x800080,
		        0x008080,
		        0x808080,
		        0xFFFBF0,
		        0xA0A0A4,
		        0x313900,
		        0xD98534
	        };


        static readonly int[] aRamka =
	        {
		        10 + (int)BoundStyle.PS_null,//0
		        10 + (int)BoundStyle.PS_DOT,//1-точки1
		        10 + (int)BoundStyle.PS_SOLID,//2-тонкая линия
		        20 + (int)BoundStyle.PS_SOLID,//3-толстая линия
		        20 + (int)BoundStyle.PS_SOLID,//4-очень толстая линия
		        10 + (int)BoundStyle.PS_SOLID,//5-двойная линия
		        10 + (int)BoundStyle.PS_DASH,//6-пунктир1
		        10 + (int)BoundStyle.PS_DASHDOT,//7-пунктир2
		        10 + (int)BoundStyle.PS_DOT,//8-точки2
		        20 + (int)BoundStyle.PS_DASH,//9-толстый пунктир
	        };

        uint GetPallete(int nIndex)
        {

            if (nIndex < 0 || nIndex >= a1CPallete.Length)
                return 0;

            uint nRes = a1CPallete[nIndex];
            return nRes; // RGB(nRes >> 16 & 0xFF, nRes >> 8 & 0xFF, nRes & 0xFF);
        }


        enum BoundStyle : int
        {
            PS_SOLID,//           0
            PS_DASH,//            1       /* -------  */
            PS_DOT,//             2       /* .......  */
            PS_DASHDOT,//         3       /* _._._._  */
            PS_DASHDOTDOT,//      4       /* _.._.._  */
            PS_null//             5
        } ;


        int GetRamka(int nIndex)
        {
            nIndex = nIndex & 15;
            if (nIndex < 0 || nIndex >= aRamka.Length)
                return (int)BoundStyle.PS_null;

            return aRamka[nIndex];
        }

        static readonly int[] aUzor =
	        {
		        33,
		        0,
		        1,
		        2,
		        22,
		        17,
		        7,
		        6,
		        14,
		        15,
		        10,
		        11,
		        31,
		        30,
		        21,
		        20
	        };

        int GetUzor(int nIndex)
        {
            nIndex = nIndex & 15;
            if (nIndex <= 0 || nIndex >= aUzor.Length)
                return 33;
            return aUzor[nIndex];
        }

    }
}
