using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;


namespace VideoProcessAnalyser
{
    [DefaultPropertyAttribute("Name")]
    [TypeConverter(typeof(RectTitleConverter))]
    public class GrabRect
    {
        private string m_name;
        private Color m_color;
        private Rectangle m_rt;
        public GrabRect(string sName, Color cCol, Rectangle prt)
        {
            Name = sName;
            Col = cCol;
            m_rt = prt;
        }
        [DisplayName("Name"), DescriptionAttribute("Name of participator")]
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        [ DisplayName("Color"), DescriptionAttribute("Color of selection")]
        public Color Col
        {
            get
            {
                return m_color;
            }
            set
            {
                m_color = value;
            }
        }
        [ DisplayName("Position"), DescriptionAttribute("Screen position")]
        public Rectangle Rect
        {
            get
            {
                return m_rt;
            }
            set
            {
                m_rt = value;
            }
        }
    }
    public class RectTitleConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
            object value, Type destType)
        {
            if (destType == typeof(String) && value is GrabRect)
            {
                GrabRect rectTitle = (GrabRect)value;
                return rectTitle.Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
