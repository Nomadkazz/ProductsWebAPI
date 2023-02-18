import { FieldValue } from "./field-value.model";
export class Product{
  id:number;
  name:string
  description:string;
  price:number;
  photoUrl:string;
  categoryId:number;
  fieldValues:FieldValue[];
}
