   M S B A M L   `   `   `  ����M  IGTA_Radios_app_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GTA_Radios_app_wpfGTA_Radios_app_wpf  5   	   #    GTA_Radios_app_wpf.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:GTA_Radios_app_wpf     5   	     � Title$<  6Radio stations of the games of Grand Theft Auto series��5   	   $	��960��6I   $
��1280��6Y   $
��1080��6i   $
��1920��6w   .��5
      � ��5   
   �� 5       �� Color$ Black��6    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��80 �6   6*    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   .+�5   
   �� ���#474747$��1��6)   $��0��69   $��Stretch=�6F   $��Stretch=�6d   $��10,10,10,10q�6�    �� Padding$ 10,10,10,10q�6�   $
��Wrap=�6�   
������$��42(�6�      
CustomFont#��[  6�   $����Welcome to application "Radio stations of the games of Grand Theft Auto series". Choose a game, then a station or search station or a track.��6
  $4��./GTA_Radios_app_wpf;component/Fonts/#Pricedown)�6�  5      �� 5   
   $	��-12q�6   $��25,25,25,25q�6    �� CornerRadius$ 50��6=   $��1��6O   $��0��6_   
���   ���5      �� 5      6$   5!      �� 5%   
   $��Stretch=�6   $��Stretch=�60   $��10,10,10,10q�6L   $��1��6a   $��1��6q   ���#474747.@�5&      �� $��	20,20,0,0q�6   
�������.��6C   siema5'      5(      �� 5)   
   $	��-12q�6   $��1��6   $��1��6.   $��25,25,25,25q�6;   $ 50��6Z   
���   ���5*      �� 5+      6$   5-      �� 51   
   $��1��6   $��2��6!   $��40��6.   $��40��69   $��2��6E   $��20(�6Y   $��Center=�6g   $��	150,0,0,0q�6�     ButtonRounded#���  6�   .��6�   ✖6�   �� 53   
   $��1��6   $��2��6!   $��40��6.   $��40��69   $��2��6E   $��20(�6Y   $��Center=�6g   $��0,0,0,0q�6�   #���  6�   .��6�   ☰6�   �� 55   
   $��1��6   $��2��6!   $��40��6.   $��40��69   $��2��6E   $��20(�6Y   $��Center=�6g   $��	0,0,150,0q�6�   #���  6�   .��6�   ↩6�   �� 59   
   $��40��6   $
��Left=�5:      $	��250��5;      $��Type station or track...��5<      $��15,5,5,5q�5=       �� TextWrapping$
 Wrap=�5>      $��Lucida Console)�5?      $��20(�5@       �� TextAlignment$
 Left=�5A      $��Center=�5B      $��1��5C      $��2��6"    J� RenderTransformOrigin$ 
-0.2,1.736&�5E        TextBoxType#���  65   �� 5F   
   $��1��6   $��2��6"   $��40��6/   $��40��6:   $��	270,0,0,0q�6F   $��20(�6Y   $
��Left=�6g   #���  6�   .��6�   🔍6�   �� 5I   
   ���#474747$��2��6)   $��0��69   $��2��6F   $��Stretch=�6W   $��Stretch=�6u   $��10,10,10,10q�6�   $ 10,10,10,10q�6�   $
��Wrap=�6�   ���#A5D58B$��42(�6�   #��[  6�   $����Welcome to application "Radio stations of the games of Grand Theft Auto series". Choose a game, then a station or search station or a track.��6  $4��./GTA_Radios_app_wpf;component/Fonts/#Pricedown)�6�  5J      �� 5K   
   $	��-12q�6   $��25,25,25,25q�6   $ 50��6=   $��2��6O   $��0��6_   $��2��6l   
���   ���5L      �� 5M      6$   5O      �� 5S   
   $��0��6   $��0��6$   $��3��61   $��15,15,10,15q�6B   $ 40��6`   ��5T      �� 5U      	 �� ImageSource$#	 Backgrounds/gta_back_left.jpg��6   6L   5W      �� 5X   
   $��3��6   $��0��6$   $��3��61   $��10,15,15,15q�6B   $ 40��6`   ��5Y      �� 5Z      $$	 Backgrounds/gta_back_right.jpg��6   6M   5\      �� 5_   
   
�������$��2��6&   $��2��66   $��Right=�6C   $��5,5,5,5q�6_   $��20(�6p   $
��Wrap=�6~   $��Bottom=�6�   Application made by �� 5a      $��Italic$�6   .�6*   Leszczyński Konrad5c      5d      5t      5u      