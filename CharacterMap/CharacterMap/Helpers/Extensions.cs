﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace CharacterMap.Helpers
{
    public static class Extensions
    {
        public static Task ExecuteAsync(this CoreDispatcher d, Func<Task> action, CoreDispatcherPriority p = CoreDispatcherPriority.Normal)
        {
            TaskCompletionSource<bool> tcs = new ();

            _ =d.RunAsync(p, async () =>
            {
                try
                {
                    await action();
                    tcs.SetResult(true);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            
            return tcs.Task;
        }

        public static Task ExecuteAsync(this CoreDispatcher d, Action action, CoreDispatcherPriority p = CoreDispatcherPriority.Normal)
        {
            TaskCompletionSource<bool> tcs = new ();

            _ = d.RunAsync(p, () =>
            {
                try
                {
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });

            return tcs.Task;
        }

        public static T AddKeyboardAccelerator<T>(this T u, VirtualKey key, VirtualKeyModifiers modifiers) where T : UIElement
        {
            u.KeyboardAccelerators.Add(new KeyboardAccelerator { Key = key, Modifiers = modifiers });
            return u;
        }

        public static T SetVisible<T>(this T e, bool b) where T : FrameworkElement
        {
            e.Visibility = b ? Visibility.Visible : Visibility.Collapsed;
            return e;
        }

        public static List<UIElement> TryGetChildren(this ItemsControl control)
        {
            //if (control.ItemsPanelRoot is null) // Calling measure forces ItemsPanelRoot to become inflated
            //    control.Measure(new Windows.Foundation.Size(100, 100));

            return new List<UIElement> { control };
        }

        public static T Realize<T>(this T list) where T : ListViewBase
        {
            if (list.ItemsPanelRoot == null)
                list.Measure(new (100, 100));

            return list;
        }
    }
}
