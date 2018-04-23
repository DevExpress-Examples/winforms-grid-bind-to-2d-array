using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace Example
{
    public class Array2DWrapper<T> : IList, ITypedList
    {
        class WrapperPropertyDescriptor : PropertyDescriptor
        {
            int index;
            Type elementType;

            public WrapperPropertyDescriptor(string name, int index, Type elementType)
                : base(name, null)
            {
                this.index = index;
                this.elementType = elementType;
            }
            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override Type ComponentType
            {
                get { return typeof(RowWrapper); }
            }

            public override object GetValue(object component)
            {
                if (component is RowWrapper) return ((RowWrapper)component).GetValue(this.index);
                return null;
            }

            public override bool IsReadOnly
            {
                get { return false; }
            }

            public override Type PropertyType
            {
                get { return elementType; }
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
                if (component is RowWrapper) ((RowWrapper)component).SetValue(this.index, value);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
        }

        class RowWrapper : CustomTypeDescriptor
        {
            Array2DWrapper<T> owner;

            public RowWrapper(Array2DWrapper<T> owner)
            {
                this.owner = owner;
            }
            public override PropertyDescriptorCollection GetProperties()
            {
                return base.GetProperties();
            }

            public object GetValue(int index)
            {
                return owner.array.GetValue(owner.list.IndexOf(this), index);
            }
            public void SetValue(int index, object value)
            {
                owner.array.SetValue(value, owner.list.IndexOf(this), index);
            }

            public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                return owner.pdc;
            }
        }

        private T[,] array;
        private List<RowWrapper> list;

        public Array2DWrapper(T[,] array)
        {
            this.array = array;
            int count = array.GetLength(0);
            list = new List<RowWrapper>(count);
            for (int i = 0; i < count; i++)
                list.Add(new RowWrapper(this));
        }

        #region IList Members

        int IList.Add(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IList.Clear()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        bool IList.Contains(object value)
        {
            if (value is RowWrapper) return list.Contains((RowWrapper)value);
            return false;
        }

        int IList.IndexOf(object value)
        {
            if (value is RowWrapper) return list.IndexOf((RowWrapper)value);
            return -1;
        }

        void IList.Insert(int index, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        bool IList.IsFixedSize
        {
            get { return true; }
        }

        bool IList.IsReadOnly
        {
            get { return true; }
        }

        void IList.Remove(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IList.RemoveAt(int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        object IList.this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            if (array is RowWrapper[]) list.CopyTo((RowWrapper[])array, index);
        }

        int ICollection.Count
        {
            get { return list.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region ITypedList Members

        PropertyDescriptorCollection pdc;

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (pdc == null)
            {
                PropertyDescriptor[] pd = new PropertyDescriptor[array.GetLength(1)];
                for (int i = 0; i < pd.Length; i++)
                {
                    pd[i] = new WrapperPropertyDescriptor("C" + i, i, typeof(T));
                }
                pdc = new PropertyDescriptorCollection(pd);
            }
            return pdc;
        }

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return string.Empty;
        }

        #endregion

    }
}