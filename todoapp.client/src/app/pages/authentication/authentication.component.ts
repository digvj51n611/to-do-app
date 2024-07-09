import { Component } from '@angular/core';
import { AuthCardComponent } from '../../components/auth-card/auth-card.component';

@Component({
  selector: 'app-authentication',
  standalone: true,
  imports: [AuthCardComponent],
  templateUrl: './authentication.component.html',
  styleUrl: './authentication.component.scss'
})
export class AuthenticationComponent {
  
}
