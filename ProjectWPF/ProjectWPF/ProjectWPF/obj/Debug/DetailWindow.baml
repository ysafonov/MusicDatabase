   M S B A M L   `   `   `  ����E  AProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ProjectWPF
ProjectWPF  5   	       ProjectWPF.DetailWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ProjectWPF     5   	   Y UXceed.Wpf.DataGrid, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f46xceed*http://schemas.xceed.com/wpf/xaml/datagrid  5   	     � Title$  Music group detail��5	   	   $	��720��6$   $	��600��61    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5         TransparentBlack �� A �� R �� G �� B��%N   &	        �� 5   
   $	 150��6)   $ 0��61   $ 0��67   $ 0��6=   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Hudební skupina��6   $��20(�5      $��10q�5       $��0��5!      $��0��5"      $��Stretch=�5#      �� 5&   
   $��0��6   $��1��6%   $��	10,0,20,0q�62   .+�5'      �� $��Nazev Skupiny:��6   $
��Bold"�6-   �� 5(      -   5        $��detailMusicGroupName��5(      $
��20,0q�64   �� 5)      $��Národnost:��6   $
��Bold"�6)   �� 5*      -   5        $!��detailMusicGroupNationality��5*      $
��20,0q�6<   �� 5+      $��
Typ hudby:��6   $
��Bold"�6)   �� 5,      -   5        $��detailMusicGroupType��5,      $
��20,0q�65   �� 5-      $��Vznik skupiny:��6   $
��Bold"�6-   �� 5.      -   5        $!��detailMusicGroupCreatedDate��5.      $
��20,0q�6;   �� 5/      $��Ukončení skupiny:��6   $
��Bold"�60   �� 50      -   5        $��detailMusicGroupEndedDate��50      $
��20,0q�69   51      �� 53   
   $��1��6   $��1��6%   $��	10,25,0,0q�62   .+�54      �� $	��180��6   $	��180��6!   .��55      (� .��56      �� -   5        $��profileImage��56      62   57      58      59      �� 5:   
   $��	Zpěváci��6   $��20(�5;      $��0��5<      $��	10,10,0,0q�5=      $��2��5>      $��Stretch=�5?      $��Bottom=�5@      (  "Xceed.Wpf.DataGrid.DataGridControl  5B   
   -   5        $��MusicianGrid��5F       $��0��5B       $��2��5C       $��3��5D       $��10q�5E       	   Columns	 5G        Xceed.Wpf.DataGrid.Column  5H      #  Xceed.Wpf.DataGrid.ColumnBase
   	FieldName$

 Name��6      Title$ Jmeno��5I         Width8  2Xceed.Wpf.DataGrid.Converters.ColumnWidthConverter$ * 5J         IsMainColumn . 5L        5M      $
 Surname��6   $ Prijmeni��5N      $ * 5O       . 5Q        5R      $
 Nationality��6   $ 	Narodnost��5S      $ * 5T       . 5V        5W      $	
 Sex��6   $ Pohlavi��5X      $ * 5Y       . 5[        5\      $
 DateOfBirth��6   $ Datum narozeni��5]      $ * 5^       . 5`        5a      $
 MusicianInstrument��6   $ 
Instrument��5b      $ * 5c       . 5e        5f      $
 ID��6      Visible .  5h      5k      �� 5l   
   $��
Písničky��6   $��20(�5m      $��0��5n      $��	10,10,0,0q�5o      $��4��5p      $��Stretch=�5q      $��Bottom=�5r        5u   
   -   5        $��SongGrid��5y       $��0��5u       $��2��5v       $��5��5w       $��10q�5x       	 5z        5{      $

 Name��6   $ Nazev��5|      $ * 5}       . 5        5�      $
 Released��6   $ Datum vydani��5�      $ * 5�       . 5�        5�      $
 Genre��6   $ 	Typ hudby��5�      $ * 5�       . "   CellEditorDisplayConditions$ Always=�5�      5�        5�      $
 Length��6   $ Delka��5�      $ * 5�       . 5�        5�      $
 Writer��6   $ 	Skladatel��5�      $ * 5�       . 5�        5�      $
 ID��6    .  5�      5�      5�      5�      