using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ChargedMinersLauncher {
    [Serializable]
    // SortableBindingList by Tim Van Wassenhove, http://www.timvw.be/presenting-the-sortablebindinglistt/
    public sealed class SortableBindingList<T> : BindingList<T> {
        private bool isSorted;
        private ListSortDirection dir = ListSortDirection.Ascending;

        [NonSerialized]
        private PropertyDescriptor sort;

        public string PreSortProperty { get; set; }


        #region BindingList<T> Public Sorting API

        public void Sort() {
            ApplySortCore( sort, dir );
        }


        public void Sort( string property ) {
            /* Get the PD */
            sort = FindPropertyDescriptor( property );

            /* Sort */
            ApplySortCore( sort, dir );
        }


        public void Sort( string property, ListSortDirection direction ) {
            /* Get the sort property */
            sort = FindPropertyDescriptor( property );
            dir = direction;

            /* Sort */
            ApplySortCore( sort, dir );
        }

        #endregion


        #region BindingList<T> Sorting Overrides

        protected override bool SupportsSortingCore {
            get { return true; }
        }


        protected override void ApplySortCore( PropertyDescriptor prop, ListSortDirection direction ) {
            List<T> items = Items as List<T>;

            if( null != items ) {
                PropertyComparer<T> pc;
                if( PreSortProperty != null ) {
                    pc = new PropertyComparer<T>( direction, new[]{ prop, FindPropertyDescriptor(PreSortProperty) } );
                } else {
                    pc = new PropertyComparer<T>( direction, prop );
                }
                items.Sort( pc );

                /* Set sorted */
                isSorted = true;
            } else {
                /* Set sorted */
                isSorted = false;
            }
        }


        protected override bool IsSortedCore {
            get { return isSorted; }
        }


        protected override void RemoveSortCore() {
            isSorted = false;
        }

        #endregion


        #region BindingList<T> Private Sorting API

        static PropertyDescriptor FindPropertyDescriptor( string property ) {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties( typeof( T ) );
            PropertyDescriptor prop = pdc.Find( property, true );
            return prop;
        }

        #endregion


        #region PropertyComparer<TKey>
        internal sealed class PropertyComparer<TKey> : IComparer<TKey> {
            /*
            * The following code contains code implemented by Rockford Lhotka:
            * //msdn.microsoft.com/library/default.asp?url=/library/en-us/dnadvnet/html/vbnet01272004.asp" href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnadvnet/html/vbnet01272004.asp">http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnadvnet/html/vbnet01272004.asp
            */

            private readonly PropertyDescriptor[] properties;
            private readonly ListSortDirection direction;

            public PropertyComparer( ListSortDirection direction, params PropertyDescriptor[] properties ) {
                this.properties = properties;
                this.direction = direction;
            }

            public int Compare( TKey xVal, TKey yVal ) {
                foreach( PropertyDescriptor property in properties ) {
                    /* Get property values */
                    object xValue = GetPropertyValue( xVal, property.Name );
                    object yValue = GetPropertyValue( yVal, property.Name );

                    /* Determine sort order */
                    int result;
                    if( direction == ListSortDirection.Ascending ) {
                        result = CompareAscending( xValue, yValue );
                    } else {
                        result = CompareDescending( xValue, yValue );
                    }
                    if( result != 0 ) return result;
                }
                return 0;
            }

            public bool Equals( TKey xVal, TKey yVal ) {
                return xVal.Equals( yVal );
            }

            public int GetHashCode( TKey obj ) {
                return obj.GetHashCode();
            }

            /* Compare two property values of any type */
            static int CompareAscending( object xValue, object yValue ) {
                int result;

                /* If values implement IComparer */
                if( xValue is IComparable ) {
                    result = ((IComparable)xValue).CompareTo( yValue );
                }
                    /* If values don't implement IComparer but are equivalent */
                else if( xValue.Equals( yValue ) ) {
                    result = 0;
                }
                    /* Values don't implement IComparer and are not equivalent, so compare as string values */
                else result = xValue.ToString().CompareTo( yValue.ToString() );

                /* Return result */
                return result;
            }

            static int CompareDescending( object xValue, object yValue ) {
                /* Return result adjusted for ascending or descending sort order ie
                   multiplied by 1 for ascending or -1 for descending */
                return CompareAscending( xValue, yValue ) * -1;
            }

            static object GetPropertyValue( TKey value, string property ) {
                /* Get property */
                PropertyInfo propertyInfo = value.GetType().GetProperty( property );

                /* Return value */
                return propertyInfo.GetValue( value, null );
            }
        }
        #endregion
    }
}
