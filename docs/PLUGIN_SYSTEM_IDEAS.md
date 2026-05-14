# Plugin And Provider System Ideas

This document is speculative. It records possible future directions for metadata providers and plugins.

## Motivation

Auralith may eventually support internet metadata enrichment, external subtitle lookup, album art providers, or other optional integrations. These should not be hardwired into early application architecture.

## Possible Provider Types

- Track metadata lookup.
- Album metadata lookup.
- Cover art lookup.
- Artist information lookup.
- Subtitle lookup.
- Media identification.

## Design Cautions

- Provider architecture should not be introduced before real use cases exist.
- Internet features should remain optional.
- Provider failures should not break local playback.
- Trust, privacy, caching, and rate limits need explicit design.

## Future Questions

- Are providers built-in modules, external plugins, or both?
- What permissions would a provider need?
- How should provider results be cached?
- How should conflicting metadata be resolved?
- Should users be able to disable providers globally or per media type?
