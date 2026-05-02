---
title: "📄 Displaying SharePoint Documents in a D365 CE Form"
date: "2020-11-17"
type: "blog"
---

Displaying SharePoint documents for an entity in Dynamics 365 CRM/CE has been a common requirement for a long time. For many years, the go-to fix was using an embedded IFrame on the form, pointed at the document relationship. However, this was always unsupported and was sure to break at some point...

It turns out it finally did. If you follow along with the original approach, seen [here](https://jlattimer.blogspot.com/2017/01/show-sharepoint-documents-on-main-form.html) on Jason Lattimer's blog, you'll find that the 'Upload' functionality now breaks if your organization has transitioned to the Unified Interface.

## The New Approach

It turns out, Microsoft has finally introduced a way to add this functionality out of the box. However, unless I've missed something, they haven't really talked about it anywhere. Hopefully, that's where this blog post comes in.

## Steps

Before starting, it is important to note that this only works on D365 CE online instances that have transitioned to the Unified Interface.

I also assume that you have already set up Sharepoint Document management for the entity in question.

1. Navigate to https://make.powerapps.com, and open the entity form that you want to add documents to.
2. Add a subgrid of 'related records' only, pointing at entity type 'Documents (Regarding)'
![Alt Text](https://dev-to-uploads.s3.amazonaws.com/i/y1tl8oeugte7enyp7fcd.png)

- Important note: this subgrid can only be added using the **new** form editor. The 'Documents' entity will not be available in the legacy version.

After adding the subgrid, save and publish your form, and open it in the Unified Interface.

You should now see a new Documents sub-grid, with the same contents as under the --> Regarding --> Documents tab.
![Alt Text](https://dev-to-uploads.s3.amazonaws.com/i/msstfoh20bu4xhi28l6c.png)

Happy documenting! ✍