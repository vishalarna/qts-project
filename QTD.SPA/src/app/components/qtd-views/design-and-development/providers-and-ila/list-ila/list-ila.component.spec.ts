import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListIlaComponent } from './list-ila.component';

describe('ListIlaComponent', () => {
  let component: ListIlaComponent;
  let fixture: ComponentFixture<ListIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
