   M S B A M L   `   `   `  ����M  IGTA_Radios_app_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GTA_Radios_app_wpfGTA_Radios_app_wpf  5          GTA_Radios_app_wpf.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      2local clr-namespace:GTA_Radios_app_wpf     5        �� 	Resources  5         AnimateTarget g� RepeatBehavior g� 	BeginTime g� Duration �� TargetProperty �� 
TargetName W� From	 W� To g� AutoReverse  AnimateTarget2  
CustomFont	 �� Property
 �� Value  FocusVisual J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background 	 Button.Pressed.Border 
 Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  ButtonRounded �� 
TargetType �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  TextBox.Static.Border  TextBox.MouseOver.Border  TextBox.Focus.Border  TextBoxType-  'System.Windows.Input.KeyboardNavigation ��Name �� Triggers ]� 
Conditions �� Property �� Value��%  &	        &	 �     &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	 /    &		 R    &	
 u    &	 �    &	 �    &	 �    7  7  7  7  7  7  7 	 7 
 7  7  &	 �    &	 �    &	 �    &	 	    7  7  7  �� 5   
   $ Forever��6+   .��5
      W� $ 0:0:0X�6   $ 0:0:0D�60   $ (ImageBrush.Opacity)�6A   $ RightImageBorder��6r   $	 1.0X�6�   $	 0.0X�6�   $ Forever��6�    . 6�   5      �� 5   
   $ Forever��6,   .��5      W� $ 0:0:0X�6   $ 0:0:0D�60   $ (ImageBrush.Opacity)�6A   $ LeftImageBorder��6r   $	 1.0X�6�   $	 0.0X�6�   $ Forever��6�    . 6�   5      �� 5   
   .�5      �� 	 � ��$
 Fonts/pricedow.ttf)�67   5      �� 5   
   .�5      �� 	 � ��
 5      �� 5      .R�5      �� $��2q�6(    . #��� ��6N   $��1��6�   $	 1 2R�6�   5      5       5!      �� 5"      $ 	#FFDDDDDD��6?   �� 5#      $ 	#FF707070��6;   �� 5$      $ 	#FFBEE6FD��6B   �� 5%      $ 	#FF3C7FB1��6>   �� 5&      $ 	#FFC4E5F6��6@   �� 5'      $ 	#FF2C628B��6<   �� 5(      $ 	#FFF4F4F4��6A   �� 5)      $ 	#FFADB2B5��6=   �� 5*      $ 	#FF838383��6A   �� 5+   
   " ��6&   .�5,      �� 	 �@�FocusVisualStyle8
   61   �� 5-      	 � ��8
  6+   �� 5.      	 � ��8
  6,   �� 5/      	 � ��#
 � ��6+   �� 50      	 � ��$
 0q�60   �� 51      	 � ��$
 Center=�6;   �� 52      	 � ��$
 Center=�69   �� 53      	 � ��$
 1q�6(   �� 54      	 � ��
 55      �� 56      " ��6&   .R�57      �� $ border��63   $ 10��6!   #��z��6C   #��z��6o   #��z��6�    . ._�58      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  59       5:      P� 5;       �@��IsDefaulted . . �5<   "   �� $ border��6@   #
 � ��6T   	 � ��5=      P� 5>       �@J�IsMouseOver . . �5?   "   �� $ border��6?   8
  	 � ��6S   �� 5@   "   $ border��6@   8
  	 � ��6T   5A      P� 5B       � �� . . �5C   "   �� $ border��6?   8
  	 � ��6S   �� 5D   "   $ border��6@   8
  	 � ��6T   5E      P� 5F       � {� .  . �5G   "   �� $ border��6?   8
  	 � ��6S   �� 5H   "   $ border��6@   8
  	 � ��6T   �� 5I   "   $ contentPresenter��6K   8
 	 	 � ��6i   5J      5L      5N      5O      �� 5Q      $ 	#FFABAdB3��6<   �� 5R      $ 	#FF7EB4EA��6?   �� 5S      $ 	#FF569DE5��6;   �� 5T      " ��6(   .�5U      �� 	 � ��#
 � ��6/   �� 5V      	 � ��8
   60   �� 5W      	 � ��#
 � ��6/   �� 5X      	 � ��$
 1q�64   �� 5Y      	 �@ TabNavigation$

 None=�6E   �� 5Z      	 � ��$

 Left=�6?   �� 5[      	 �@�FocusVisualStyle
 65   Z� �� 5\      	 �@J�	AllowDrop
 . 6.   �� 5]      	 �@��PanningMode$
 VerticalFirst=�6=   �� 5^      	 �@��IsFlicksEnabled
 .  6;   �� 5_      	 � ��
 5`      �� 5a      " ��6*   .R�5b      �� $ border��67   $ 10��6%   #��z��6G   #��z��6s   #��z��6�    . ._�5c   "   �� $ PART_ContentHost��6/   |�.  $��Hidden=�6[   $��Hidden=�6�   5d       5e      P� 5f   "    � {� .  . �5g   &   �� $ border��6@   	 �@J�Opacity$

 0.56Q�6T   5h   #   P� 5i   "    �@J�IsMouseOver . . �5j   &   �� $ border��6D   8
  	 � ��6X   5k   #   P� 5l   "    �@J�IsKeyboardFocused . . �5m   &   �� $ border��6D   8
  	 � ��6X   5n   #   5p      5r       5s      ]� 5t       5u      �� 5v      + �@��#IsInactiveSelectionHighlightEnabled . 6W   �� 5w       �@��IsSelectionActive .  6E   ./�5y      �� 	 �@��SelectionBrush#
 � /�6;   5z      5|      5~      5      