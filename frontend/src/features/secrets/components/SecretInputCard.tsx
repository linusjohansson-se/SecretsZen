import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { FieldContent, FieldDescription, FieldGroup, FieldLabel, FieldLegend, FieldSeparator, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Slider } from "@/components/ui/slider";

export default function SecretInputCard() {
  const expiryText = "test"
  const viewDestructText = "test2"

  return (
    <FieldGroup className="flex flex-col w-full max-w-full">
      <FieldSet>
        <FieldLegend>Generate link for secret</FieldLegend>
        <FieldDescription>All secrets are encrypted and only stored while link is active</FieldDescription>
        <FieldGroup>
          <FieldSet>
            <FieldLabel>Password or secret</FieldLabel>
            <Input />
          </FieldSet>
        </FieldGroup>
        <FieldGroup>
          <FieldContent>
            <div className="flex flex-row justify-between">
              <FieldLabel>Expire after</FieldLabel>
              <FieldLabel>{expiryText}</FieldLabel>
            </div>
            <Slider />
            <FieldDescription>Link expires after {expiryText}</FieldDescription>
          </FieldContent>
        </FieldGroup>
        <FieldGroup>
          <FieldContent>
            <div className="flex flex-row justify-between">
              <FieldLabel>Max views</FieldLabel>
              <FieldLabel>{viewDestructText}</FieldLabel>
            </div>
            <Slider />
            <FieldDescription>Link self-destructs after {viewDestructText}</FieldDescription>
          </FieldContent>
        </FieldGroup>
        <Button title="Generate Secure Link">Generate Secure Link</Button>
      </FieldSet>
    </FieldGroup >
  )

}
