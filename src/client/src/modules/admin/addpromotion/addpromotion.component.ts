import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCamera, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { PromotionService } from '../../../services/promotion/promotion-service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-addpromotion',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, FormsModule],
  templateUrl: './addpromotion.component.html',
  styleUrl: './addpromotion.component.css'
})
export class AddpromotionComponent {
  public faCamera: IconDefinition = faCamera;
  
  promotion = {
    promotionTitle: '',
    description: '',
    discount: 0,
    startDate: '',
    endDate: ''
  };
  selectedFile: File | null = null;
  imagePreviewUrl: string | ArrayBuffer | null = null;

  constructor(private promotionService: PromotionService, private router: Router) {}

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file && file.type.startsWith('image/')) {
      this.selectedFile = file;
      
      // Create preview for the selected image
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreviewUrl = reader.result;
      };
      reader.readAsDataURL(file);
    } else {
      alert('Please select a valid image file');
      this.selectedFile = null;
      this.imagePreviewUrl = null;
    }
  }

  onSubmit() {
    event?.preventDefault(); // Prevent page reload when submitting form

    const formData = new FormData();
    formData.append('PromotionTitle', this.promotion.promotionTitle);
    formData.append('Description', this.promotion.description);
    formData.append('Discount', this.promotion.discount.toString());
    formData.append('StartDate', this.promotion.startDate);
    formData.append('EndDate', this.promotion.endDate);
    
    if (this.selectedFile) {
      formData.append('Image', this.selectedFile);
    }

    console.log(formData);

    this.promotionService.createPromotion(formData).subscribe({
      next: () => {
        alert('Promotion created successfully!');
        this.router.navigate(['admin/promotionmanagement']);
      },
      error: (err) => {
        console.error('Error creating promotion:', err);
        alert('Failed to create promotion. Please try again.');
      }
    });
  }

  cancel() {
    this.router.navigate(['admin/promotionmanagement']);
  }
}