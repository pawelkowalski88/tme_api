using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TME_API_v1
{
    public class GetPricesAndStock
    {

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class response
        {

            private string statusField;

            private responseData dataField;

            /// <remarks/>
            public string Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            public responseData Data
            {
                get
                {
                    return this.dataField;
                }
                set
                {
                    this.dataField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class responseData
        {

            private string currencyField;

            private string languageField;

            private string priceTypeField;

            private responseDataProduct[] productListField;

            /// <remarks/>
            public string Currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
                }
            }

            /// <remarks/>
            public string PriceType
            {
                get
                {
                    return this.priceTypeField;
                }
                set
                {
                    this.priceTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Product", IsNullable = false)]
            public responseDataProduct[] ProductList
            {
                get
                {
                    return this.productListField;
                }
                set
                {
                    this.productListField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class responseDataProduct
        {

            private string symbolField;

            private responseDataProductPrice[] priceListField;

            private string unitField;

            private byte vatRateField;

            private string vatTypeField;

            private uint amountField;

            /// <remarks/>
            public string Symbol
            {
                get
                {
                    return this.symbolField;
                }
                set
                {
                    this.symbolField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Price", IsNullable = false)]
            public responseDataProductPrice[] PriceList
            {
                get
                {
                    return this.priceListField;
                }
                set
                {
                    this.priceListField = value;
                }
            }

            /// <remarks/>
            public string Unit
            {
                get
                {
                    return this.unitField;
                }
                set
                {
                    this.unitField = value;
                }
            }

            /// <remarks/>
            public byte VatRate
            {
                get
                {
                    return this.vatRateField;
                }
                set
                {
                    this.vatRateField = value;
                }
            }

            /// <remarks/>
            public string VatType
            {
                get
                {
                    return this.vatTypeField;
                }
                set
                {
                    this.vatTypeField = value;
                }
            }

            /// <remarks/>
            public uint Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class responseDataProductPrice
        {

            private ushort amountField;

            private decimal priceValueField;

            private byte priceBaseField;

            private object specialField;

            /// <remarks/>
            public ushort Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }

            /// <remarks/>
            public decimal PriceValue
            {
                get
                {
                    return this.priceValueField;
                }
                set
                {
                    this.priceValueField = value;
                }
            }

            /// <remarks/>
            public byte PriceBase
            {
                get
                {
                    return this.priceBaseField;
                }
                set
                {
                    this.priceBaseField = value;
                }
            }

            /// <remarks/>
            public object Special
            {
                get
                {
                    return this.specialField;
                }
                set
                {
                    this.specialField = value;
                }
            }
        }

    }
}
