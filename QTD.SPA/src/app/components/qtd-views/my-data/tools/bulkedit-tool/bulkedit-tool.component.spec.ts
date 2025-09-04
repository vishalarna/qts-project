import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkeditToolComponent } from './bulkedit-tool.component';

describe('BulkeditToolComponent', () => {
  let component: BulkeditToolComponent;
  let fixture: ComponentFixture<BulkeditToolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulkeditToolComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkeditToolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
