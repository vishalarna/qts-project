import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIlaComponent } from './create-ila.component';

describe('CreateIlaComponent', () => {
  let component: CreateIlaComponent;
  let fixture: ComponentFixture<CreateIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
