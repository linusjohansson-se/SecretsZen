import { Button } from "@/components/ui/button";
import { FieldDescription, FieldGroup, FieldLabel, FieldLegend, FieldSeparator, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Slider } from "@/components/ui/slider";
import { useState } from "react";

export default function SecretInputCard() {
  const [expiryDays, setExpiryDays] = useState(7);
  const [maxViews, setMaxViews] = useState(10);
  const [link, setLink] = useState("aa");

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
          <FieldSet>
            <div className="flex flex-row justify-between">
              <FieldLabel>Expire after</FieldLabel>
              <span className="text-sm text-muted-foreground">{expiryDays}{expiryDays > 1 ? " days" : " day"}</span>
            </div>
            <Slider defaultValue={[expiryDays]} min={1} max={30} onValueChange={(val) => setExpiryDays(Array.isArray(val) ? val[0] : val)} />
          </FieldSet>
          <FieldSet>
            <div className="flex flex-row justify-between">
              <FieldLabel>Max views</FieldLabel>
              <span className="text-sm text-muted-foreground">{maxViews}{maxViews > 1 ? " views" : " view"}</span>
            </div>
            <Slider defaultValue={[maxViews]} min={1} max={100} onValueChange={(val) => setMaxViews(Array.isArray(val) ? val[0] : val)} />
          </FieldSet>
        </FieldGroup>
        <Button>Generate Secure Link</Button>
      </FieldSet>
      {link && (
        <>
          <FieldSeparator />
          <FieldGroup>
            <FieldSet>
              <FieldLabel>Your secure link</FieldLabel>
              <Input value={link} readOnly />
            </FieldSet>
          </FieldGroup>
        </>
      )}
    </FieldGroup>
  )

}
