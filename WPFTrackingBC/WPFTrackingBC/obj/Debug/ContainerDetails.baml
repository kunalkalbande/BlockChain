   M S B A M L   `   `   `  ����H  DWPFTrackingBC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WPFTrackingBCWPFTrackingBC  5   	   $    WPFTrackingBC.ContainerDetails   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WPFTrackingBC     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   3    � WindowStyle$
 None=�5       � 
ResizeMode$ NoResize=�6/    � Title$ ContainerDetails��5   	   $	��340��6"   $	��340��6/   .��5	      �� 
���   �$��1q�6!   ._�5
   
   � ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ White��6#    � Offset$ 0Q�61   � 5      $ 	LightGray��6#   $ 1Q�65   5       � RowDefinitions 5      �� 5      $��40 �6    �� 5      $��* �6    .+�5      � ���#074A63$��Stretch=�6(   .+�5      �� $��Container Details��6   $
��Bold"�65   ���#69C6F1	 �� TextAlignment$	 Center=�6\   $	��Top=�6s   
 �� Padding$

 0 10q�6�   5      � 5      $��1��6    5      �� 5      $��20 �6$   �� 5      $��35 �6$   �� 5      $��35 �6$   �� 5      $��35 �6$   �� 5      $��35 �6$   �� 5      $��35 �6$   �� 5       $��35 �6$   �� 5!       � ColumnDefinitions 5#      �� 5$      $	��110 �6'   �� 5%      .+�5'      �� $��ContainerId:��6   $��Right=�60   $��Center=�6L   $��0 10 0 0q�6g   $y�	Collapsed=�6y   �� 5(      -   5         ��Name$ txtContainerId��5(      $��1��62   $��
12 20 12 5q�6B   {�.  $y�	Collapsed=�6h   �� 5)      $��Container Name:��6   $��1��63   $��Right=�6@   $��Center=�6\   �� 5*      -   5        $ txtContainerName��5*      $��1��64   $��1��6D   $
��12 5q�6Q   �� 5+      $��Container Weight:��6   $��2��65   $��Right=�6B   $��Center=�6^   �� 5,      -   5        $ txtContainerWeight��5,      $��1��66   $��2��6F   $
��12 5q�6S   �� 5-      $��Material Quantity:��6   $��3��66   $��Right=�6C   $��Center=�6_   �� 5.      -   5        $ txtQuantity��5.      $��1��6/   $��3��6?   $
��12 5q�6L   �� 5/      $��Source:��6   $��4��6+   $��Right=�68   $��Center=�6T   �� 50      -   5        $ 	txtSource��50      $��1��6-   $��4��6=   $
��12 5q�6J   �� 51      $��Destination:��6   $��5��60   $��Right=�6=   $��Center=�6Y   �� 52      -   5        $ txtDestination��52      $��1��62   $��5��6B   $
��12 5q�6O   � 55      $��7��6   $��2��6$    56      �� 57      �� 58      .+�5:      �� -   5         ��Name$ btnClose��5:      ���#074A63
�������$��Close��6X   $��0��6h   $��3��6x   $��Center=�6�   $��Center=�6�   $��90��6�   $
��15 5q�6�   $��5q�6�   6�   �� 5;      -	   5        $ btnSave��5;      ���#074A63
�������$��Initiate��6W   $��1��6j   $��3��6z   $��Center=�6�   $��Center=�6�   $��90��6�   $
��15 5q�6�   $��5q�6�   5<      5=      5>      5?      5@      