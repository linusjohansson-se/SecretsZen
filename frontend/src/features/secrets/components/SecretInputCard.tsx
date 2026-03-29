import { Button } from "@/components/ui/button";
import { FieldDescription, FieldGroup, FieldLabel, FieldLegend, FieldSeparator, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Slider } from "@/components/ui/slider";
import { useEffect, useState } from "react";
import useCreateSecret from "../hooks/useCreateSecret";
import { Spinner } from "@/components/ui/spinner";
import { toast } from "sonner";

export default function SecretInputCard() {
    const [expiryDays, setExpiryDays] = useState(7);
    const [maxViews, setMaxViews] = useState(10);
    const [link, setLink] = useState("");
    const [secret, setSecret] = useState("");

    const { mutate, isPending } = useCreateSecret();

    const handleGenerateLink = () => {
        if (!secret) {
            toast.error("Please enter a secret or password.");
            return;
        }

        mutate(
            { expiryDays: expiryDays, maxViews: maxViews, password: secret },
            {
                onSuccess: (data) => {
                    if (data.ok) {
                        data.json().then((res) => {
                            setLink(`${window.location.origin}/s/${res.id}`);
                        });
                    } else {
                        // Handle error response
                        console.error("Failed to create secret");
                        toast.error("Failed to create secret. Please try again later.");
                    }
                },
                onError: (error) => {
                    console.error("An error occurred:", error);
                    toast.error("Failed to create secret. Please try again.");
                },
            }
        );
    };

    return (
        <FieldGroup>
            <FieldSet>
                <FieldLegend>Generate link for secret</FieldLegend>
                <FieldDescription>All secrets are encrypted and only stored while link is active</FieldDescription>
                <FieldGroup>
                    <FieldSet>
                        <FieldLabel>Password or secret</FieldLabel>
                        <Input onChange={(e) => setSecret(e.target.value)}/>
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
                {isPending ? <Button disabled><Spinner />Generating...</Button> : <Button onClick={() => handleGenerateLink()}>Generate Secure Link</Button>}
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
