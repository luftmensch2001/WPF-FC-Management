   M S B A M L   `   `   `  ����>  :FCM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FCM.UserControlsFCM.UserControls  5          FCM.UserControls.ucPlayer   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      *localclr-namespace:FCM.UserControls  5      -   5          >�Name$  wdPlayer��5
         PlayerVM � DataContext# [  5      .��5      � $��50��6    � 	Resources 5   
    �� 
TargetType �� Property �� Value��%�   '��       �� 5      " ��6   .�5      ��  � ��
 �����6/   �� 5       � ��$ Center=�68   �� 5       � ��$ Center=�66   �� 5       � ��$ Medium"�6/   5      5       � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $
��1.8* �6   �� 5      $
��0.6* �6   �� 5      �� 5      �� 5      �� 5      $��0.82* �6   .+�5    
   � $��0��6   .+�5!      �� -   5         ��Name$ tblIndex��5"      $��1��5#      �� 5$      
�������$��25��6$   $��1��60   $��Right=�6:   5%      � 5&   
   $��1��6   .+�5'      �� -   5        $ tblName��5(      $��WrapWithOverflow=�5)       �� TextAlignment$ Center=�5*      $��Nguyễn Thị Long��5+      �� 5,      
�������$��25��6$   $��1��60   $��Right=�6:   5-      � 5.   
   $��2��6   .+�5/      �� -   5        $ 	tblNumber��50      $��12��51      �� 52      
�������$��25��6$   $��1��60   $��Right=�6:   53      � 54   
   $��3��6   .+�55      �� -   5        $ tblPosition��56      $��Tiền vệ��57      �� 58      
�������$��25��6$   $��1��60   $��Right=�6:   59      � 5:   
   $��4��6   .+�5;      �� -   5        $ tblDoB��5<      $��
31/02/1999��5=      �� 5>      
�������$��25��6$   $��1��60   $��Right=�6:   5?      � 5@   
   $��5��6   .+�5A      �� -   5        $ tblNationality��5B      $��WrapWithOverflow=�5C      $ Center=�5D      $��
Việt Nam��5E      �� 5F      
�������$��25��6$   $��1��60   $��Right=�6:   5G      � 5I   
   $��6��6   .+�5J      �� 	 �� Orientation$	 
Horizontal=�6   $��10 0 0 0q�62   .+�5K      �� -   5        
 ��Name$
 btnEditInfor��5L        MyGreenButtonStyle#��[ 5M      ���#4554ff$��0q�5O      $��24��5P      $��24��6    $��0��5Q      .��5R      '  !MaterialDesignThemes.Wpf.PackIcon  $��15��5S      $��15��5T         Kind$ InformationOutline=�5U      .  (System.Windows.Interactivity.Interaction   Triggers 5V      /  )System.Windows.Interactivity.EventTrigger  5W         	EventName$ Click��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5X      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *EditPlayerCommand+   CommandParameter 5Y   %   ��  �� ElementName$ wdPlayer��5Z      5\      �� 5]      -	   5        $
 btnDeletePlayer��5^        MyRedButtonStyle#��[ 5_      $��0q�5`      $��24��5a      $��24��6    $��0��5b      $��5 0 0 0q�6"   .��5c        $��15��5d      $��15��5e      $ DeleteOutline=�5f       5g        5h      $ Click��6)   . 5i         64   �� *DeletePlayerCommand+ 5j   %   �� $ wdPlayer��5k      5m      5n      5o      �� 5p   
   $��7��6   
�������$��1��64   $��Bottom=�6@   5q      5r      