﻿// ****************************************************************************
// <copyright file="ApplicationBarBehavior.cs" company="Pedro Lamas">
// Copyright © Pedro Lamas 2011
// </copyright>
// ****************************************************************************
// <author>Pedro Lamas</author>
// <email>pedrolamas@gmail.com</email>
// <date>17-11-2011</date>
// <project>Cimbalino.Phone.Toolkit</project>
// <web>http://www.pedrolamas.com</web>
// <license>
// See license.txt in this solution or http://www.pedrolamas.com/license_MIT.txt
// </license>
// ****************************************************************************

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Cimbalino.Phone.Toolkit.Behaviors
{
    /// <summary>
    /// The behavior that creates a bindable <see cref="Microsoft.Phone.Shell.ApplicationBar" />
    /// </summary>
    [System.Windows.Markup.ContentProperty("Buttons")]
    public class ApplicationBarBehavior : SafeBehavior<FrameworkElement>
    {
        private readonly IApplicationBar _applicationBar = new ApplicationBar();

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBarBehavior" /> class.
        /// </summary>
        public ApplicationBarBehavior()
        {
            Buttons = new ApplicationBarIconButtonCollection(_applicationBar.Buttons);
            MenuItems = new ApplicationBarMenuItemCollection(_applicationBar.MenuItems);

            _applicationBar.StateChanged += new EventHandler<ApplicationBarStateChangedEventArgs>(ApplicationBar_StateChanged);
        }

        /// <summary>
        /// Occurs when the user opens or closes the menu.
        /// </summary>
        public event EventHandler<ApplicationBarStateChangedEventArgs> StateChanged;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            AssociatedObject.LayoutUpdated += AssociatedObject_LayoutUpdated;

            base.OnAttached();
        }

        /// <summary>
        /// Gets the list of the menu items that appear on the Application Bar.
        /// </summary>
        /// <value>The list of menu items.</value>
        public ApplicationBarMenuItemCollection MenuItems
        {
            get
            {
                return (ApplicationBarMenuItemCollection)GetValue(MenuItemsProperty);
            }
            private set
            {
                SetValue(MenuItemsProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="MenuItems" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuItemsProperty =
            DependencyProperty.Register("MenuItems", typeof(ApplicationBarMenuItemCollection), typeof(ApplicationBarBehavior), null);

        /// <summary>
        /// Gets the list of the buttons that appear on the Application Bar.
        /// </summary>
        /// <value>The Application Bar buttons.</value>
        public ApplicationBarIconButtonCollection Buttons
        {
            get
            {
                return (ApplicationBarIconButtonCollection)GetValue(ButtonsProperty);
            }
            private set
            {
                SetValue(ButtonsProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="Buttons" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ButtonsProperty =
            DependencyProperty.Register("Buttons", typeof(ApplicationBarIconButtonCollection), typeof(ApplicationBarBehavior), null);

        /// <summary>
        /// Gets or sets the background color of the Application Bar.
        /// </summary>
        /// <value>The background color of the Application Bar.</value>
        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="BackgroundColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(ApplicationBarBehavior), new PropertyMetadata(OnBackgroundColorChanged));

        /// <summary>
        /// Called after the background color of the ApplicationBar is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject" />.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        protected static void OnBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is Color)
            {
                ((ApplicationBarBehavior)d)._applicationBar.BackgroundColor = (Color)e.NewValue;
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the Application Bar.
        /// </summary>
        /// <value>The foreground color of the Application Bar.</value>
        public Color ForegroundColor
        {
            get
            {
                return (Color)GetValue(ForegroundColorProperty);
            }
            set
            {
                SetValue(ForegroundColorProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="ForegroundColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register("ForegroundColor", typeof(Color), typeof(ApplicationBarBehavior), new PropertyMetadata(OnForegroundColorChanged));

        /// <summary>
        /// Called after the foreground color of the ApplicationBar is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject" />.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        protected static void OnForegroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is Color)
            {
                ((ApplicationBarBehavior)d)._applicationBar.ForegroundColor = (Color)e.NewValue;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can open the menu.
        /// </summary>
        /// <value>true if the menu is enabled; otherwise, false.</value>
        public bool IsMenuEnabled
        {
            get
            {
                return (bool)GetValue(IsMenuEnabledProperty);
            }
            set
            {
                SetValue(IsMenuEnabledProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="IsMenuEnabled" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsMenuEnabledProperty =
            DependencyProperty.Register("IsMenuEnabled", typeof(bool), typeof(ApplicationBarBehavior), new PropertyMetadata(true, OnIsMenuEnabledChanged));

        /// <summary>
        /// Called after the menu enabled state of the Application Bar is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject" />.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        protected static void OnIsMenuEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is bool)
            {
                ((ApplicationBarBehavior)d)._applicationBar.IsMenuEnabled = (bool)e.NewValue;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Application Bar is visible.
        /// </summary>
        /// <value>true if the Application Bar is visible; otherwise, false.</value>
        public bool IsVisible
        {
            get
            {
                return (bool)GetValue(IsVisibleProperty);
            }
            set
            {
                SetValue(IsVisibleProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="IsVisible" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(ApplicationBarBehavior), new PropertyMetadata(true, OnIsVisibleChanged));

        /// <summary>
        /// Called after the visible state of the Application Bar is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject" />.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        protected static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is bool)
            {
                ((ApplicationBarBehavior)d)._applicationBar.IsVisible = (bool)e.NewValue;
            }
        }

        /// <summary>
        /// Gets or sets the size of the Application Bar.
        /// </summary>
        /// <value>One of the enumeration values that indicates the size of the Application Bar.</value>
        public ApplicationBarMode Mode
        {
            get
            {
                return (ApplicationBarMode)GetValue(ModeProperty);
            }
            set
            {
                SetValue(ModeProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="Mode" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ApplicationBarMode), typeof(ApplicationBarBehavior), new PropertyMetadata(ApplicationBarMode.Default, OnModeChanged));

        /// <summary>
        /// Called after the size of the ApplicationBar is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject" />.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        protected static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is ApplicationBarMode)
            {
                ((ApplicationBarBehavior)d)._applicationBar.Mode = (ApplicationBarMode)e.NewValue;
            }
        }

        /// <summary>
        /// Gets or sets the opacity of the Application Bar.
        /// </summary>
        /// <value>The opacity of the Application Bar.</value>
        public double Opacity
        {
            get
            {
                return (double)GetValue(OpacityProperty);
            }
            set
            {
                SetValue(OpacityProperty, value);
            }
        }

        /// <summary>
        /// Identifier for the <see cref="Opacity" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register("Opacity", typeof(double), typeof(ApplicationBarBehavior), new PropertyMetadata(1.0, OnOpacityChanged));

        /// <summary>
        /// Called after the opacity of the ApplicationBar is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject" />.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        protected static void OnOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is double)
            {
                ((ApplicationBarBehavior)d)._applicationBar.Opacity = (double)e.NewValue;
            }
        }

        /// <summary>
        /// Gets or sets the command to invoke when the user opens or closes the menu. This is a DependencyProperty.
        /// </summary>
        /// <value>The command to invoke when the user opens or closes the menu. The default is null.</value>
        public ICommand StateChangedCommand
        {
            get { return (ICommand)GetValue(StateChangedCommandProperty); }
            set { SetValue(StateChangedCommandProperty, value); }
        }

        /// <summary>
        /// Identifier for the <see cref="StateChangedCommand" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty StateChangedCommandProperty =
            DependencyProperty.Register("StateChangedCommand", typeof(ICommand), typeof(ApplicationBarBehavior), null);

        private void AssociatedObject_LayoutUpdated(object sender, EventArgs e)
        {
            SetApplicationBar();
        }

        private void ApplicationBar_StateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            var eventHandler = StateChanged;

            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }

            var command = StateChangedCommand;

            if (command != null)
            {
                var commandParameter = e;

                if (command.CanExecute(commandParameter))
                {
                    command.Execute(commandParameter);
                }
            }
        }

        internal void SetApplicationBar()
        {
            AssociatedObject.LayoutUpdated -= AssociatedObject_LayoutUpdated;

            if (DesignerProperties.IsInDesignTool)
            {
                return;
            }

            var page = AssociatedObject.Parent as PhoneApplicationPage;

            if (page == null)
            {
                throw new Exception("This ApplicationBarBehavior element can only be attached to the LayoutRoot element");
            }

            page.ApplicationBar = _applicationBar;
        }
    }
}