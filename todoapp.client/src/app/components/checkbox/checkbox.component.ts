import { Component , Output,Input, EventEmitter, ViewChild, ElementRef} from '@angular/core';

@Component({
  selector: 'app-checkbox',
  standalone: true,
  imports: [],
  templateUrl: './checkbox.component.html',
  styleUrl: './checkbox.component.scss'
})
export class CheckboxComponent {
  @Input({required : true}) checked : boolean = false;
  @Output() changeEvent = new EventEmitter<boolean>();
  onChange(event : Event ) {
    var target = event.currentTarget as HTMLInputElement;
    this.changeEvent.emit(target.checked);
  }
}
