   M S B A M L   `   `   `  ����M  IGTA_Radios_app_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GTA_Radios_app_wpfGTA_Radios_app_wpf  5   	   #    GTA_Radios_app_wpf.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:GTA_Radios_app_wpf     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Icon$  Covers/Basics/app_icon.ico��5	   	    � Title$$ Grand Theft Auto Radios&Tracks��5
   	   $	��720��65   $
��1280��6E   $
��1080��6U   $
��1920��6c   .��5      � ��5   
   �� 5       �� Color$ Black��6    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��2* �6   6*   �� 5      $��80 �6   6*    � ColumnDefinitions 5   
   �� 5      $
��478* �6   6.   �� 5      $
��426* �6   6.   �� 5      $	��53* �6   �� 5      $
��478* �6   6.   �� 5      $
��478* �6   6.   .+�5   
   �� $��1��6   $��0��6'   $��Disabled=�64   $
��Auto=�6]   ��. $��10,15,10,10q�6�   $��2��6�   .��5       �� -   5        $��InfoBox��5       ���#474747$��Stretch=�6>   $��Stretch=�6\    �� Padding$ 10,10,10,10q�6y   $
��Wrap=�6�   
�������$��42(�6�      
CustomFont#��[  6�   $4��./GTA_Radios_app_wpf;component/Fonts/#Pricedown)�6�   ��Welcome to application "Radio stations of the games of Grand Theft Auto series". Choose a game, then a station or search station or a track.5"      5#      �� 5&   
   -   5        $��MainList��5&   d   $��0q�6    �� SelectionMode$ Single=�6'   $��Stretch=�6t   $��Stretch=�6�   $��10,15,10,10q�6�   $��3��6�   $��0��6�   $��2��6�   ���#474747��5'      �� 5(       �� 
TargetType" ��6   	 �� BasedOn#	 [B��61   .�5)      -   5        5*      .@�5-      �� $��20(�6   
�������.��6?   siema6F   5.      �� 52   
   -   5        $��
QuitButton��52      $��3��6#   $��2��63   $��40��6@   $��40��6K   $��20(�6W   $��Right=�6e   $��0,0,10,0q�6�     ButtonRounded#���  6�   .��6�   ✖6�   �� 54   
   -   5        $��
MenuButton��54      $��3��6#   $��2��63   $��40��6@   $��40��6K   $��20(�6W   $��Right=�6e   $��
0,20,85,20q�6�   #���  6�   .��6�   ☰6�   �� 56   
   -   5        $��
BackButton��56   #   {�.  $��3��65   $��2��6E   $��40��6R   $��40��6]   $��20(�6i   $��Right=�6w   $��0,20,160,20q�6�   #���  6�   .��6�   ↩6�   �� 5:   
   -   5        $��	SearchBox��5;      $��40��5<      $
��Left=�5=      $	��250��5>      $��
15,20,0,20q�5?      
 �� TextWrapping$

 Wrap=�5@      $��Lucida Console)�5A      $��20(�5B       �� TextAlignment$
 Left=�5C      $��Center=�5D      $��1��5E      $��2��6    J� RenderTransformOrigin$ 
-0.2,1.736&�5F        TextBoxType#���  5G      63   �� 5H   
   -   5        $��SearchButton��5H      $��1��6%   $��2��66   $��40��6C   $��40��6N   $��270,20,0,20q�6Z   $��20(�6o   $
��Left=�6}   #���  6�   .��6�   🔍6�   �� 5L   
   ��. $��10,10,10,10q�60   ���#474747$��Stretch=�6]   $��Stretch=�6{   $��1��6�   $��1��6�   $
��auto=�6�   $��Disabled=�6�   $��2��6  .��5N      �  5O      �� 5P      $��* �6$   61   �� 5Q      $
��Auto �6$   64    5S      �� 5T      $��* �6'   63   .+�5V      �� -	   5        $��AltText��5V   N   $��0��6   $y�Hidden=�6)   $��0��6>   
�������$	��Top=�6o   $��Center=�6�   $��NO IMAGE��6�   $��42(�6�   #��[  6�   $4��./GTA_Radios_app_wpf;component/Fonts/#Pricedown)�6�   6$  �� 5W      -
   5        $��
CoverPlace��5W   5   $��0��6   $��0��6%   $ ��Covers/Basics/mainlogo.png��6H   $	��250��6l   $	��400��6y   $��Center=�6�   5Y      �� 5Z      -   5        $��ItemInfoBox��5Z   \   $��1��6   $��0��6)   $
��auto��6:   $��10,10,10,10q�6G   $
��Wrap=�6o   $��42(�6�   #��[  6�   $4��./GTA_Radios_app_wpf;component/Fonts/#Pricedown)�6�   6�   5[      5\      �� 5`   
   -   5        $��LeftImageBorder��5`      $��0��6)   $��0��6:   $��3��6G   $��15,15,10,15q�6X    �� CornerRadius$ 40��6v   ��5a      �� 5b      -   5        LefttImageBrush��5b   :   ._�5d      �� 5e      �� 5f   
   -   5        $��RightImageBorder��5f      $��4��6)   $��0��6:   $��3��6G   $��10,15,15,15q�6X   $ 40��6v   ��5g      �� 5h      -   5        RightImageBrush��5i      5k      r� 5l   
   -   5        $��aPlayer��5l   C   $y�Hidden=�6    r� LoadedBehavior$ Manual=�6+    r� Source$g aC:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Sounds\menu_down.wav?�6R   6�   5m      5n      