   M S B A M L   `   `   `  ����E  AProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ProjectWPF
ProjectWPF  5   	       ProjectWPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Y UXceed.Wpf.DataGrid, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f46xceed*http://schemas.xceed.com/wpf/xaml/datagrid  5   	   $localclr-namespace:ProjectWPF  5   	   X TXceed.Wpf.Toolkit, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4<xceedToolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  Music Groups��5
   	   $	��500��6   $	��800��6+    � WindowStartupLocation$ CenterScreen=�67    � 	Resources 5         TransparentBlack �� A �� R �� G �� B��%N   &	        �� 5   
   $	 150��6)   $ 0��61   $ 0��67   $ 0��6=   5      .��5      � -   5        $��
LayoutRoot��5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��9* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��3��5      
�������5      o� 5   
   $��0��6   $��0��5      $��18(�5       
������ $��Center=�5"      $��Stretch=�5#      $��Stretch=�5$      .@�5&      m� -   5        $��_File��5&      $��	10,0,10,0q�5(      .@�5*      m� -   5        $��_Save��5*      $��1��6*   5+      5,      m� 5-      $
��_CSV��6   $��	10,0,10,0q�5.      .@�50      m� -   5        $��_Export��50      $��1��6,   51      m� 52      -   5        $��_Import��52      $��1��6,   53      54      57      o� 58   
   $��2��6   $��18(�59      
������ $��Center=�5;      $��Right=�5<      .@�5=      �� -   5        	 ��Name$	 textBoxFind��5=      $	��150��5>      m� 5@      -   5        $��_Find��5@      $��	10,0,10,0q�5A      5B      5C      (  "Xceed.Wpf.DataGrid.DataGridControl  5F   
   -   5        $��MusicGroupGrid��5J       $��0��5F       $��3��5G       $��1��5H       $��60,30,60,20q�5I       
   Columns
 5K        Xceed.Wpf.DataGrid.Column  5L      #  Xceed.Wpf.DataGrid.ColumnBase   	FieldName$
 Name��6      Title$ Nazev Skupiny��5M         Width8  2Xceed.Wpf.DataGrid.Converters.ColumnWidthConverter$ * 5N         IsMainColumn . 5P        5Q      $ Nationality��6   $ 	Narodnost��5R      $ * 5S       . 5U        5V      $
 Type��6   $ 	Typ hudby��5W      $ * 5X       . "   CellEditorDisplayConditions$ Always=�5Z         
CellEditor 5[      #  Xceed.Wpf.DataGrid.CellEditor  5\         EditTemplate 5]      �� 5^   "   .R�5_   &   ��    ProjectWPF.EnumLists   TypeOfMusicList#��Z 6/    �� SelectedItem 5`   /   :  4Xceed.Wpf.DataGrid.Markup.CellEditorBindingExtension  5a   #   5c      5e        5f      $ CreatedDate��6   $ Datum zahajeni��5g      $ * 5h       . 5j        5k      $ EndDate��6   $ Datum ukonceni��5l      $ * 5m       . 5o        5p      $ Image��6      Visible .  5r        5s      $ ID��6    .  5u      5x      �� 5y   
   -	   5        $��DeleteButton��5~      $��2��5y      $��2��5z      $��Delete��5{      $��10,10,60,20q�5|      $��10q�5}      5      �� 5�   
   -
   5        $��
ViewButton��5�      $��1��5�      $��2��5�      $��10q�5�      $��35,10,35,20q�5�      $��Detail��5�      5�      �� 5�   
   -   5        $��	AddButton��5�      $��0��5�      $��2��5�      $��10q�5�      $��60,10,10,20q�5�      $	��Add��5�      5�      5�      5�      