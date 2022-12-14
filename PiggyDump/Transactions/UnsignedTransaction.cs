/*
    Copyright (c) 2019 SaladBadger

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Descent2Workshop.Transactions
{
    public class UnsignedTransaction : Transaction
    {
        private uint oldValue;
        private uint newValue;
        public UnsignedTransaction(string label, object target, string propertyName, int page, int tab, uint newValue) : base(label, target, propertyName, page, tab)
        {
            this.newValue = newValue;
        }

        public override bool Apply()
        {
            //okay so this is a dumb hack so we can generalize this mess
            if (property.PropertyType == typeof(ushort))
            {
                //Preserve the old value for undo purposes
                oldValue = (uint)((ushort)property.GetValue(target));
                //Set the new value
                property.SetValue(target, (ushort)newValue);
            }
            else if (property.PropertyType == typeof(byte))
            {
                //Preserve the old value for undo purposes
                oldValue = (uint)((byte)property.GetValue(target));
                //Set the new value
                property.SetValue(target, (byte)newValue);
            }
            else
            {
                //Preserve the old value for undo purposes
                oldValue = (uint)property.GetValue(target);
                //Set the new value
                property.SetValue(target, newValue);
            }
            return oldValue != newValue; //this is a little bad, but the results should not matter. Not that this is very ACID anyways...
        }

        public override void Revert()
        {
            //okay so this is a dumb hack so we can generalize this mess
            if (property.PropertyType == typeof(ushort))
            {
                property.SetValue(target, (ushort)oldValue);
            }
            else if (property.PropertyType == typeof(byte))
            {
                property.SetValue(target, (byte)oldValue);
            }
            else
            {
                property.SetValue(target, oldValue);
            }
        }

        public override object GetOldValue()
        {
            return oldValue;
        }
    }
}
