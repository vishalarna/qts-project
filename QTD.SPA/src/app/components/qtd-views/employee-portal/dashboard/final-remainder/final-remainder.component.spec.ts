import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinalRemainderComponent } from './final-remainder.component';

describe('FinalRemainderComponent', () => {
  let component: FinalRemainderComponent;
  let fixture: ComponentFixture<FinalRemainderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FinalRemainderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FinalRemainderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
