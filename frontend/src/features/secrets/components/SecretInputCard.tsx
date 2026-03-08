import { Button } from "@/components/ui/button";
import { FieldDescription, FieldGroup, FieldLabel, FieldLegend, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Slider } from "@/components/ui/slider";

export default function SecretInputCard() {
  const expiryText = "test"
  const viewDestructText = "test2"

  return (
    <>
      <FieldGroup>
        <FieldSet>
          <FieldLegend>Generate link for secret</FieldLegend>
          <FieldDescription>All secrets are encrypted and only stored while link is active</FieldDescription>
          <FieldGroup>
            <FieldSet>
              <FieldLabel>Password or secret text</FieldLabel>
              <Input />
            </FieldSet>
          </FieldGroup>
          <FieldGroup>
            <Slider />
          </FieldGroup>
          <FieldGroup>
            <Slider />
          </FieldGroup>
          <Button />
        </FieldSet>
      </FieldGroup >
    </>
  )

}
