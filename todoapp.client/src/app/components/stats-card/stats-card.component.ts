import { NgClass, NgIf, PercentPipe } from '@angular/common';
import { Component,Input } from '@angular/core';

@Component({
  selector: 'app-stats-card',
  standalone: true,
  imports: [NgClass,PercentPipe,NgIf],
  templateUrl: './stats-card.component.html',
  styleUrl: './stats-card.component.scss'
})
export class StatsCardComponent {
  @Input({required : true}) isActiveCard : boolean = false;
  @Input({required : true}) ratioValue : number = 0;
}
