# EF Core Mapping Conventions

Shows that if some conventions are followed
no detailed mappings are needed and EF Core is smart enough
to infer a lot of the values.

Note that in the mapping code no navigation properties need
to be mapped -- mapping the underlying keys suffices.

## SQLite Database

- Two tables , `efc_artist` and `efc_album`
  - Table prefix "efc_"
  - Name is singular
- PK is "table_name" + "_id": `efc_artist_id`
- FK are "target_table_name" + "_id": `efc_album.efc_artist_id`
- Property that would be PascalCase in C# are snake_case in db: `artist_name == ArtistName`

```sql
sqlite> create table efc_artist ( efc_artist_id integer primary key, artist_name text);
sqlite> create table efc_album (efc_album_id integer primary key, efc_artist_id integer, title text, foreign key (efc_artist_id) references efc_artist(efc_artist_id));
sqlite> insert into efc_artist values (1, 'artist one');
sqlite> insert into efc_artist values (2, '2wo');
sqlite> insert into efc_album values (1, 1, 'one - one');
sqlite> insert into efc_album values (2, 1, 'one - two');
sqlite> insert into efc_album values (3, 2, '2 x 1');
```
