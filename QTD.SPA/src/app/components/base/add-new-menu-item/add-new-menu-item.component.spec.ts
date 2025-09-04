import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewMenuItemComponent } from './add-new-menu-item.component';

describe('AddNewMenuItemComponent', () => {
  let component: AddNewMenuItemComponent;
  let fixture: ComponentFixture<AddNewMenuItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNewMenuItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNewMenuItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
