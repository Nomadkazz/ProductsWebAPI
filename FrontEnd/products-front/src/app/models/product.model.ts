import { FieldValue } from "./field-value.model";
export class Product{
  id:number;
  name:string
  description:string;
  price:number;
  photo_url:string;
  category_id:number;
  field_values:FieldValue[];
}
