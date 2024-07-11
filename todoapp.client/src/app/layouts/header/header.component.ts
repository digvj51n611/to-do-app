import { Component } from '@angular/core';
import { ButtonIconComponent } from '../../components/button-icon/button-icon.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [ButtonIconComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  heading : string = "Dashboard";
}
