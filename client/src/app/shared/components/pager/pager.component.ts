import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number; // Input Property is something we receive from Parent Conponent.
  @Output() pageChange = new EventEmitter<number>(); // Child Component can emit output to Parent Conponent.

  @Input() pageNumber: number;

  constructor() { }

  ngOnInit(): void {
  }

  onPagerChange(event:any){
    this.pageChange.emit(event.page);
  }

}
