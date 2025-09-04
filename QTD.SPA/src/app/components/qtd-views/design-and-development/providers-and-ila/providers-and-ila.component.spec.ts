import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProvidersAndIlaComponent } from './providers-and-ila.component';

describe('ProvidersAndIlaComponent', () => {
  let component: ProvidersAndIlaComponent;
  let fixture: ComponentFixture<ProvidersAndIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProvidersAndIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProvidersAndIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
