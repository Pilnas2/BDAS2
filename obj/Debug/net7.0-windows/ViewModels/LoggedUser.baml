   M S B A M L   `   `   `  ����I  ESemPrace_BDAS2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:SemPrace_BDAS2.ViewModelsSemPrace_BDAS2.ViewModels  5      *    $SemPrace_BDAS2.ViewModels.LoggedUser   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:SemPrace_BDAS2.ViewModels     5        N� Title$  
LoggedUser��5	      $��Center=�6   .��5      � ���	#FFF35F5F$	��550��6"   $��
0,0,-515,0q�6/    � ColumnDefinitions 5   
   �� 5      $	��38* �6   �� 5      $
��187* �6   �� 5      $��0* �6   .+�5   
   �� ���	#FFDAC1C1.+�5      � $	��550��6   .+�5      �� -   5         ��Name$ buttonBackLogin��5      $��Zpět na prihlášení��62   $��9,414,11,110q�6O   $��25��6�   �� 5      $
��MENU��6   $��Center=�6'   $��0,10,0,0q�6D   $	��Top=�6V   $��14(�6n   $
��Bold"�6|   �� 5      -   5        $ buttonCitizents��5      $��Občané��62   $��11,155,9,370q�6C   $��25��6w   �� 5      -   5        $ buttonOffices��5      $��Pobočky��60   $��10,125,10,400q�6^   �� 5      $��
0,454,2,71q�6   
���   �$��25��6C   �� 5      -   5        $ buttonInsurance��5      $��Typy Pojištění��62   $��11,185,9,340q�6i   $��25��6   �� 5      -   5        $ 	buttonEnd��5      $��Ukončit��6,   $��12,484,10,41q�6W   $��25��6m   �� 5      -   5        $ buttonInsuranceCard��5      $��Průkaz Pojišťovny��66   $��10,215,11,310q�6t   $��25��6�   5      5      &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ DataGrid��5       $��1��6%   $��0,87,0,0q�65   �� 5!   
   -   5        $ 
buttonEdit��5!      $��1��6%   $��Upravit��65   $
��Left=�6G   $��
137,28,0,0q�6b   $	��Top=�6v   $��54��6�   6�    � 5#   
   -	   5        $��	mainFrame��5#        � NavigationUIVisibility$ Hidden=�6!   5$      5%      