   M S B A M L   `   `   `  ����F  BFBGEMSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:FBGEMSystem.RealtimeStatusFBGEMSystem.RealtimeStatus  5   	   Y UWPFVisifire.Charts, Version=1.0.1.0, Culture=neutral, PublicKeyToken=366fc8dc0356ba75M9clr-namespace:Visifire.Charts;assembly=WPFVisifire.ChartsVisifire.Charts 5   	   -    'FBGEMSystem.RealtimeStatus.ElectricShow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:FBGEMSystem.RealtimeStatus     5   	   Hvc9clr-namespace:Visifire.Charts;assembly=WPFVisifire.Charts     5   	   -   5          � Title$!  电类传感器单点曲线��5	   	   $	��554��6   $��976.5��6(    � Icon$. (/FBGEMSystem;component/Image/Globe32.png��6M   .��5
      � $��0,10,2,0q�6   $	��509��6   $	��Top=�6*    � ColumnDefinitions 5   
   �� 5      $	��37* �6   �� 5      $
��101* �6   .+�5   
   �� $��0��6   
������� J� RenderTransformOrigin$ 	0.5,0.467&�6=   $��Stretch=�6_   $��-1,2,1,1q�6}   z�5      V� 5      .~�5      �� �� 5       �� AngleX$ 0.233Q�6$   �� 5      T� 5       T� X$
 0.93Q�6)   5      .+�5      �� -   5         ��Name$ lbl_Elc��5      $��类型选择��6%   $��16(�64   $
��Left=�6C   $	��Top=�6^   $��	91,23,0,0q�6v   $��36��6�   $��79��6�   $ 0.425,2.556&�6�   �� 5      -   5         ��Name$ comboBox_typeNum��5      $��Right=�63   $��	0,23,75,0q�6O   $	��Top=�6b   $	��109��6z   $��22��6�   $ 0.557,3.073&�6�   �� 5      -   5        $ lbl_channel1��5      $��通道选择��6*   $��16(�69   $	��Top=�6G   $��
91,18,89,0q�6_   $��36��6s   �� 5      -   5        $ comboBox_CHNum��5      $��Right=�61   $��	0,23,75,0q�6M   $	��Top=�6`   $	��109��6x   $��22��6�   $ 0.557,3.073&�6�   5      � 5   
   -   5         �Name$ 
LayoutRoot��5      $��1��6#   $��0,0,0,0q�63   $��Stretch=�6D   $��Stretch=�6b   .+�5        Visifire.Charts.Chart  -   5        	  Name$	 chart��5      $��0��6)   $��0��69   
   
Background

 �����   CornerRadius$ 10,10,10,10��6_      Theme$ Theme1��6z      ShadowEnabled .    ScrollingEnabled$
 TrueX�6�    J� 	AllowDrop . $��0,10,10.4,1q�6�   $��Stretch=�6�   $��Stretch=�6�      AxesY 5!        Visifire.Charts.Axis  -   5          Name$ SingleAy��5!         Title$  ��61      TitleFontColor
 ��  �   TitleFontSize$ 18Q�6X   5"         AxesX 5%        -	   5        $ SingleAx��5%         ClosestPlotDistance$ 0Q�61   $  ��6J   $ 18Q�6S      Interval$
 1000X�6f   5&         Series 5)         Visifire.Charts.DataSeries  -
   5          Name$ ds��5)   $      RenderAs$ 	QuickLine=�61      Bevel$
 TrueY�6G   $��Stretch=�6T      AutoFitToPlotArea .    Color
 ��  �   ShadowEnabled$
 TrueY�6�    J� SnapsToDevicePixels .   . 6�   5+      5-      5.      5/      