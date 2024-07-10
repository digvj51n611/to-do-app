import { NgClass } from '@angular/common';
import { Component , Input } from '@angular/core';

@Component({
  selector: 'app-button-icon',
  standalone: true,
  imports: [NgClass],
  templateUrl: './button-icon.component.html',
  styleUrl: './button-icon.component.scss'
})
export class ButtonIconComponent {
  @Input({required : true}) buttonName! : string;
  @Input() bgClassList : string = '';
  @Input() btnClassList : string = '';
}
