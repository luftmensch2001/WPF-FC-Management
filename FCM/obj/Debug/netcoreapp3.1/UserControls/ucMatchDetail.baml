   M S B A M L   `   `   `  ����>  :FCM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FCM.UserControlsFCM.UserControls  5      $    FCM.UserControls.ucMatchDetail   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:FCM.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      -   5          >�Name$  wcMatchDetailCard��5      ���#282823
�������$	��Top=�5         EditMatchInforVM � DataContext# [  5      $��0 25 0 0q�5      .��5      �  � 	Resources 5   
     
headerText �� 
TargetType  MaterialDesignTextBlock �� BasedOn �� Property �� Value��%�   &	       7  �� 5      " ��6'   8   .�5      ��  � �� �#fff6/   �� 5       � ��$ 16(�6-   �� 5       � ��$ Center=�66   �� 5       � ��$ Center=�68   �� 5       � ��$ Medium"�6/   5      5       � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $
��2.5* �6   �� 5       $��2* �6   �� 5!      $
��2.5* �6   �� 5"      $��2* �6   �� 5#      $
��1.6* �6   �� 5$      $��2* �6   .+�5'   
   � $��0��6   .+�5(      �� -   5         ��Name$ tblSTT��5)      $��No��5(      #��[ 5*      �� 5+      $��30��6   $��1��6$   
�������$��Right=�6:   5,      � 5.   
   $��1��6   .+�5/      �� -   5        $ tblDate��50      $��Ngày��5/      #��[ 51      �� 52      $��30��6   $��1��6$   
�������$��Right=�6:   53      � 55   
   $��2��6   .+�56      �� -   5        $ tblTime��57      $��Giờ��56      #��[ 58      �� 59      $��30��6   $��1��6$   
�������$��Right=�6:   5:      � 5<   
   $��3��6   .+�5=      �� -   5        $ tblTeam1��5>      $��Đội 1��5=      $��WrapWithOverflow=�5?      	 �� TextAlignment$	 Center=�5@      #��[ 5A      �� 5B      $��30��6   $��1��6$   
�������$��Right=�6:   5C      � 5E   
   $��4��6   .+�5F      �� -   5        $ tblScore��5G      $��	Tỉ số��5F      #��[ 5H      �� 5I      $��30��6   $��1��6$   
�������$��Right=�6:   5J      � 5L   
   $��5��6   .+�5M      �� -   5        $ tblTeam2��5N      $��Đội 2��5M      $��WrapWithOverflow=�5O      $	 Center=�5P      #��[ 5Q      �� 5R      $��30��6   $��1��6$   
�������$��Right=�6:   5S      � 5U   
   $��6��6   .+�5V      �� -   5        $ 
tblStadium��5W      $
��Sân��5V      $��WrapWithOverflow=�5X      $	 Center=�5Y      #��[ 5Z      �� 5[      $��30��6   $��1��6$   
�������$��Right=�6:   5\      � 5^   
   $��7��6   .+�5_      �� -	   5        $ tblRound��5`      $��Vòng đấu��5_      $��WrapWithOverflow=�5a      $	 Center=�5b      #��[ 5c      �� 5d      $��30��6   $��1��6$   
�������$��Right=�6:   5e      � 5g   
   $��8��6   $��70��6   $	��Top=�6+   .+�5h      �� $��0 0 0 5q�6   .+�5i      �� -
   5        
 ��Name$
 btnRecordResult��5j        MyGreenButtonStyle#��[ 5k      $��70��5l      $��18��5m      $��0��5n      $��0q�5o      .��5q      ��  �� Orientation$ 
Horizontal=�6!   .+�5r      '  !MaterialDesignThemes.Wpf.PackIcon  $��13��5s      $��13��5t         Kind$ FileEdit=�5u      $��0 0 4 0q�5v      �� 5w      $��Ghi KQ��6$   $��10(�5x       5y      .  (System.Windows.Interactivity.Interaction   Triggers 5z      /  )System.Windows.Interactivity.EventTrigger  5{         	EventName$ Click��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5|      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *OpenResultRecordWindowCommand+   CommandParameter 5}   8   ��  �� ElementName$ wcMatchDetailCard��5~      5�      �� 5�      -   5        $
 btnEditInfor��5�      #��[ 5�      ���#4554ff$��70��5�      $��18��5�      $��0��5�      $��0q�5�      .��5�      �� $ 
Horizontal=�6!   .+�5�        $��13��5�      $��13��5�      $ Pencil=�5�      $��0 0 4 0q�5�      �� 5�      $��Sửa TT��6$   $��10(�5�       5�       5�        5�      $ Click��6)   . 5�         64   �� *OpenEditMatchWindowCommand+ 5�   8   �� $ wcMatchDetailCard��5�      5�      �� 5�      -   5        $
 btnCancelResult��5�        MyRedButtonStyle#��[ 5�      $��70��5�      $��18��5�      $��0��5�      $��0q�5�      .��5�      �� $ 
Horizontal=�6!   .+�5�        $��13��5�      $��13��5�      $ 	CloseBold=�5�      $��0 0 4 0q�5�      �� 5�      $��Hủy KQ��6$   $��10(�5�       5�       5�        5�      $ Click��6)   . 5�         64   �� *CancelResultCommand+ 5�   8   �� $ wcMatchDetailCard��5�      5�      5�      5�      �� 5�   
   $��9��6   ���#ccc$��1��64   $��Bottom=�6?   $��0 0 0 0q�6Z   5�      5�      